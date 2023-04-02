using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using VkParser.Models;
using VkParser.src.Sql;

namespace VkParser.Extenthions
{
    internal static class WebElementsExtenthions
    {
        public static async Task<List<PlaylistModel>> FindBestPlaylist(this List<IWebElement> playlist, IWebDriver driver,string target)
        {
           int i = 0;
           var pixels = 0;
           var queue=new Queue<PlaylistModel>();            
            foreach (var item in playlist)
            {
                i++;
                queue.Enqueue(Task.Run(() =>  Parse(item,driver,target)).Result);
                if (i%4==0)
                {
                    driver.ExecuteJavaScript($"window.scrollBy({pixels},{500})", "");
                    pixels += 500;
                }  
            }
            var result = queue.Where(x=>x.Played>50000).ToList();
            return result;
        }
        public static  Task SaveData(this List<PlaylistModel> sortedPlaylist)
        {
            var sql=new SqlCrud();
            foreach (var item in sortedPlaylist)
            {
                sql.LoadData(item.Name,item.AuthorId,item.Played,item.Target);
            }
            return Task.CompletedTask;
        }
        private static async Task<PlaylistModel> Parse(IWebElement item, IWebDriver driver,string target)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var finalData = new PlaylistModel();
            var authorId = "";
            var name = "";
            var played = 0;
                
                try
                {
                    Thread.Sleep(1000);
                    var buttonwaiter = new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName(item.TagName)));
                    item.Click();
                    var formwaiter = new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[15]/div/div[1]/div/div[1]/div/div[3]/div[1]/div[3]/a")));
                    authorId = item.FindElement(By.XPath("/html/body/div[15]/div/div[1]/div/div[1]/div/div[3]/div[1]/div[3]/a")).GetAttribute("href");
                    name = item.FindElement(By.XPath("/html/body/div[15]/div/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/a/span[1]")).Text;
                    var a = new string(item.FindElement(By.XPath("/html/body/div[15]/div/div[1]/div/div[1]/div/div[3]/div[1]/div[4]")).Text.Take(8).ToArray()).Replace(" ", string.Empty);
                    a = rgx.Replace(a, string.Empty);
                    played = int.Parse(a);
                    finalData = new PlaylistModel() { AuthorId = authorId, Name = name, Played = played,Target=target };
                    driver.Navigate().Back();
                    Console.WriteLine("Complete");


                }
                catch(WebDriverException ex)                
                {
                    Console.WriteLine("Fault");
                    Console.WriteLine(ex.Message);
                    
                   

            }
                return finalData;

            
        }
    }
}

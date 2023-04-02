using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkParser.src;

namespace VkParser
{
    internal class TaskManager
    {
        public static  Task StartParse(IWebDriver [] driver,string [] targets)
        {
            Parallel.For(0, targets.Length, async (i) =>
            {
                driver[i] = new ChromeDriver();
                Thread.Sleep(4000);
                await CreateInstanse(targets[i], driver[i]);
            });
            return Task.CompletedTask;
        }
        private static async Task CreateInstanse(string targetName, IWebDriver driver)
        {


            Auth auth = new Auth(driver);            
            Target target = new Target(driver, targetName);
            driver.Navigate().GoToUrl("https://vk.com");
            Thread.Sleep(2000);
            auth.Authificate();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://vk.com/audios39783398/");        
            await target.Parse();
            Console.WriteLine();
        }
    }
}

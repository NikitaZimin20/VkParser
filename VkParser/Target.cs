using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkParser.Extenthions;

namespace VkParser.src
{
    internal class Target : IParser
    {
        private readonly IWebDriver _driver;
        private readonly string  _target;
        public Target(IWebDriver driver,string target)
        {
            _driver= driver;
            _target= target;
        }
        public async Task Parse()
        {
            FindTarget(); //ищем страницу с плейлистами
            Thread.Sleep(1000);
             await  FindAlbums(); //собираем информацию из плейлистов
        }
        private  void FindTarget()
        {
            try
            {
                _driver.Navigate().GoToUrl($"{_driver.Url}?q={_target}");
                var formwaiter = new WebDriverWait(_driver, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("audio_page_block__show_all_link_label")));
                _driver.FindElement(By.ClassName("audio_page_block__show_all_link_label")).Click();
            }
            catch (WebDriverException ex)
            {

               _driver.Close();
            }
           
        }
        private async Task FindAlbums()
        {
            Thread.Sleep(1000);
            var allPlaylists=new List<IWebElement>();
            for (int i = 1000; i < 3000; i+=1000)
            {
                MakePlaylist(ref allPlaylists,i.ToString());
            }
            allPlaylists.Distinct();
            _driver.ExecuteJavaScript($"window.scrollBy(0,{-3000})", "");
            var bestplaylists= await allPlaylists.FindBestPlaylist(_driver,_target);//смотреть папку Extenthions
            await bestplaylists.SaveData();//смотреть папку Extenthions
            _driver.Close();
        }
        private void MakePlaylist(ref List<IWebElement> allPlaylists,string scrollPixels)
        {         
            _driver.ExecuteJavaScript($"window.scrollBy(0,{scrollPixels})", "");
            var playlists = _driver.FindElements(By.ClassName("audio_pl_item2"));
            Thread.Sleep(2000);
            foreach (var playlistItem in playlists)
            {
                allPlaylists.Add(playlistItem);
            }
        }
    }
}

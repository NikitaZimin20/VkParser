using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkParser.src
{
    internal class Auth
    {
        private readonly JsonDeserializer _deserializer;
        private readonly IWebDriver _driver;
        public Auth(IWebDriver driver)
        {
            _deserializer = new JsonDeserializer();
            _driver = driver;
        }

        internal void Authificate()
        {
            ValidateLogin();
            Thread.Sleep(2000);
            ValidatePassword();
        }
        private void ValidateLogin()
        {
            SubmitForm( "//*[@id=\"index_email\"]", "Login");

        }
        private void ValidatePassword()
        {
            SubmitForm("//*[@id=\"root\"]/div/div/div/div/div/div/div/div/form/div[1]/div[3]/div[1]/div/input", "Password");

        }
        private void SubmitForm( string className, string deserializer)
        {

            var param = _deserializer.DeserializeByName<string>(deserializer);
            var formwaiter = new WebDriverWait(_driver, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(className)));
            var form = _driver.FindElement(By.XPath(className));
            form.Click();
            form.Clear();
            form.SendKeys(param);
            form.Submit();

        }
    }
}

using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VkParser;
using VkParser.src;

JsonDeserializer deserializer = new JsonDeserializer();
var targets = deserializer.DeserializeByName<string[]>("Targets");
var chuncks = targets.Chunk(5);
IWebDriver [] webDrivers = new ChromeDriver[targets.Length];
var tasks= new Task[targets.Length/5];
for (int i = 0; i < targets.Length/5; i++)
{
    tasks[i]= Task.Factory.StartNew(()=>  TaskManager.StartParse(webDrivers, chuncks.ElementAt(i)));
    tasks[i].Wait();
}





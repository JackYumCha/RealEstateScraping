using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net;
using System.Net.Http;
using System.Linq;
using OpenQA.Selenium.Support.UI;



namespace RealEstateScraping
{
    public class SearchGoogleForAvatars
    {
        public void SearchAvatar()
        {



            ChromeDriver chromeDriver = new ChromeDriver();

            chromeDriver.Url = "https://google.com";

            chromeDriver.Navigate();

            var wait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 30));

            wait.Until(driver => driver.FindElement(By.CssSelector("")));
            var inputSearch = chromeDriver.FindElementById("lst-ib");

            inputSearch.SendKeys("证件照");

            inputSearch.SendKeys("\n");


            //var searchBtn = chromeDriver.FindElementByCssSelector("input[name=\"btnK\"]");
            //searchBtn.Click();


            var navBar = chromeDriver.FindElementById("hdtb-msb-vis");

            var piclink = navBar.FindElement(By.CssSelector("a"));

            piclink.Click();

            Debugger.Break();

            var picContainer = chromeDriver.FindElementById("rg");

            var imageElements = picContainer.FindElements(By.CssSelector("img"));

            List<string> urls = new List<string>();

            foreach(var el in imageElements)
            {

                urls.Add(el.GetAttribute("src"));
            }

            WebClient client = new WebClient();

            int i = 0;

            string baseFolder = AppContext.BaseDirectory;

            foreach(var url in urls)
            {
                i++;

                if (url.StartsWith("data:"))
                {

                }
                else
                {
                    //client.DownloadFileTaskAsync(url, $@"{baseFolder}\{i}.jpg");
                    var bytes = client.DownloadData(url);

                    System.IO.File.WriteAllBytes($@"{baseFolder}\{i}.jpg", bytes);
                }
            }
        }

    }
}

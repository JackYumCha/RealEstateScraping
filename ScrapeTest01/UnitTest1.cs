using System;
using System.Diagnostics;
using Xunit;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using System.Web;

namespace ScrapeTest01
{
    public class UnitTest1
    {
        [Fact(DisplayName ="Scrape Stage 3 900g")]
        public async void Test1()
        {
            int j = 0;
            int z = 0;
            int i = 0;
            var client = new HttpClient();
            var result = await client.GetAsync(@"https://www.chemistwarehouse.com.au/buy/69967/a2-premium-toddler-stage-3-900g");
            var html = await result.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var priceNodes = doc.DocumentNode.DescendantsAndSelf().Where(n => n.Name.ToLower() == "div" && n.HasClass("Price")).ToList();
            
            string priceRaw = priceNodes.First().InnerText;
            priceRaw = priceRaw.Replace(" ", "").Replace("\n", "");

            Debug.WriteLine(priceRaw);

            throw new Exception();
        }

        [Fact(DisplayName ="Scrape All A2 Baby Formula")]
        public async void ScrapeA2BabyFormula()
        {
            //

            string searchURL = @"https://www.chemistwarehouse.com.au/search/go?w=a2%20stage";

            var client = new HttpClient();
            var result = await client.GetAsync(searchURL);
            var html = await result.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var priceNodes = doc.DocumentNode.DescendantsAndSelf()
                .Where(n => n.Name.ToLower() == "a" 
                && n.HasClass("product-container")
                && n.HasClass("sli_content_link")).ToList();

            var urls = priceNodes
                .Select(node => node.GetAttributeValue("href", ""))
                .Where(url => url != "")
                .ToList();

            foreach(var url in urls)
            {

                //https://www.chemistwarehouse.com.au/search/go?p=R&srid=S1-1SYDP&lbc=chemistwarehouse&w=a2%20stage&
                //url=https%3a%2f%2fwww.chemistwarehouse.com.au%2fbuy%2f69967%2fa2-premium-toddler-stage-3-900g&lgsku=69967&rk=1&uid=944960698&sid=101&ts=custom&SLIPid=1538294496124&rsc=tvQwX103lGnjDA-0&method=and&isort=score&view=grid

                var innerUrl = url.Substring(url.IndexOf("url=")+4);
                innerUrl = WebUtility.UrlDecode(innerUrl);

                var resultPrd = await client.GetAsync(url);
                var htmlPrd = await resultPrd.Content.ReadAsStringAsync();
                var docPrd = new HtmlDocument();
                docPrd.LoadHtml(htmlPrd);



                var priceNodesPrd = docPrd
                    .DocumentNode
                    .DescendantsAndSelf()
                    .Where(n => n.Name.ToLower() == "div" && n.HasClass("Price"))
                    .ToList();

                string priceRaw = priceNodesPrd.First().InnerText;
                priceRaw = priceRaw.Replace(" ", "").Replace("\n", "");

                Debug.WriteLine(priceRaw);
            }

            Debugger.Break();
        }


        [Theory]
        [InlineData("john")]
        [InlineData("mary")]
        public void Test2(string name)
        {

        }
    }

}

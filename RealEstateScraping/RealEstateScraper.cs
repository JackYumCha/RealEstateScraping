using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RealEstateScraping
{
    public class RealEstateScraper
    {
        
        public async Task<RealRootObject> GetHousesInMapBoundary()
        {
            var url = @"https://services.realestate.com.au/services/listings/summaries/search?query={%22channel%22:%22buy%22,%22filters%22:{%22surroundingSuburbs%22:%22true%22,%22excludeTier2%22:%22true%22,%22geoPrecision%22:%22address%22,%22excludeAddressHidden%22:%22true%22},%22boundingBoxSearch%22:[-27.581652399341113,153.0498269147863,-27.57703067323637,153.07591944408318],%22pageSize%22:%22500%22}";
            //var url = @"https://ecommerce-deploy-98-api.prod.pizzahutaustralia.com.au/api/services/app/store/GetTradingStoresStates";


            //CookieContainer cookieContainer = new CookieContainer();

            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync(url);

            //    var json = await response.Content.ReadAsStringAsync();
            //    var result = JsonConvert.DeserializeObject<RootObject>(json);
            //    return result;
            //}

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) )
            {
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RealRootObject>(json);
                return result;
            }


        }

    }
}

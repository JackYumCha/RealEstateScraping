using System;
using Xunit;
using RealEstateScraping;
using System.Diagnostics;
using System.Linq;

namespace RealEstateScraping.Tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Test")]
        public async void Test1()
        {
            RealEstateScraper scraper = new RealEstateScraper();
            //var result = await scraper.GetHousesInMapBoundary();

            var result = scraper.GetHousesInMapBoundary().GetAwaiter().GetResult();

            // var storesInQLD = result.result.stores.Where(store => store.state.ToLower() == "qld");

            Debugger.Break();

        }
    }
}

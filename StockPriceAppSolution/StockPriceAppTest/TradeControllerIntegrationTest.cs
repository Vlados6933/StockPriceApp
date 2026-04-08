using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Test;

namespace Tests
{
    public class TradeControllerIntegrationTest(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        #region Index

        [Fact]
        public async Task Index_ToReturnView()
        {
            HttpResponseMessage response = await _client.GetAsync("/Trade/Index/MSFT");

            Assert.True(response.IsSuccessStatusCode);

            string responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseBody);
            var document = html.DocumentNode;

            Assert.NotNull(document.QuerySelectorAll(".price"));
        }

        #endregion
    }
}
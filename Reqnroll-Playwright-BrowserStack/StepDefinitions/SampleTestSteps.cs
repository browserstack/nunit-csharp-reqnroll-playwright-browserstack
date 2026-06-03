using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;

namespace ReqnrollPlaywrightBrowserStack
{
    [Binding]
    public class SampleTestSteps
    {
        private readonly IPage _page;
        private string? _productOnPageText;

        public SampleTestSteps(ScenarioContext scenarioContext)
        {
            _page = scenarioContext.ScenarioContainer.Resolve<IPage>();
        }

        [Given(@"I navigate to the StackDemo website")]
        public async Task GivenINavigateToTheStackDemoWebsite()
        {
            await _page.GotoAsync("https://bstackdemo.com/");
        }

        [When(@"I add the first product to the cart")]
        public async Task WhenIAddTheFirstProductToTheCart()
        {
            _productOnPageText = await _page.Locator("//*[@id=\"1\"]/p").InnerTextAsync();
            await _page.Locator("//*[@id=\"1\"]/div[4]").ClickAsync();
        }

        [When(@"the cart pane is opened")]
        public async Task WhenTheCartPaneIsOpened()
        {
            await _page.Locator(".float-cart__content").WaitForAsync();
        }

        [Then(@"the product in the cart matches the product on the page")]
        public async Task ThenTheProductInTheCartMatchesTheProductOnThePage()
        {
            string productInCartText = await _page
                .Locator("//*[@id=\"__next\"]/div/div/div[2]/div[2]/div[2]/div/div[3]/p[1]")
                .InnerTextAsync();
            Assert.That(productInCartText, Is.EqualTo(_productOnPageText));
        }
    }
}

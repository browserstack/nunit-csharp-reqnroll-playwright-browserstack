using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;

namespace ReqnrollPlaywrightBrowserStack
{
    [Binding]
    public class SampleLocalTestSteps
    {
        private readonly IPage _page;

        public SampleLocalTestSteps(ScenarioContext scenarioContext)
        {
            _page = scenarioContext.ScenarioContainer.Resolve<IPage>();
        }

        [Given(@"I navigate to the local website")]
        public async Task GivenINavigateToTheLocalWebsite()
        {
            await _page.GotoAsync("http://bs-local.com:45454");
        }

        [Then(@"the page title should be ""(.*)""")]
        public async Task ThenThePageTitleShouldBe(string expectedTitle)
        {
            string title = await _page.TitleAsync();
            Assert.That(title, Is.EqualTo(expectedTitle));
        }
    }
}

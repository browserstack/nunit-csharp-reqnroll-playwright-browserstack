using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace ReqnrollPlaywrightBrowserStack
{
    // Manages the Playwright browser/page lifecycle for each scenario.
    // The BrowserStack SDK intercepts the Playwright connection and routes it
    // to the BrowserStack cloud using the platforms defined in browserstack.yml.
    [Binding]
    public class PlaywrightHooks
    {
        private readonly ScenarioContext _scenarioContext;

        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private IPage? _page;

        public PlaywrightHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            // Expose the page to the step definitions via the scenario container.
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<IPage>(_page);
        }

        [AfterScenario]
        public async Task TearDownAsync()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
            }
            if (_context != null)
            {
                await _context.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            _playwright?.Dispose();
        }
    }
}

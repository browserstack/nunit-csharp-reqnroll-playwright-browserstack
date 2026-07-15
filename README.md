# nunit-csharp-reqnroll-playwright-browserstack

This sample shows how to run [NUnit](https://nunit.org/) + [Reqnroll](https://reqnroll.net/) + [Playwright](https://playwright.dev/dotnet) tests on BrowserStack using the [BrowserStack .NET SDK](https://www.nuget.org/packages/BrowserStack.TestAdapter). The SDK reads `browserstack.yml`, fans your scenarios out across the platforms listed there, starts and stops BrowserStack Local automatically, and reports test status to the BrowserStack dashboard. Your test code stays pure `Microsoft.Playwright` + Reqnroll -- no manual `ConnectAsync`, no caps in code.

![BrowserStack Logo](https://d98b8t1nnulk5.cloudfront.net/production/images/layout/logo-header.png?1469004780)

## Prerequisites

* [.NET SDK 8.0](https://dotnet.microsoft.com/download) (the project targets `net8.0`)
* A BrowserStack account -- grab your [Username and Access Key](https://www.browserstack.com/accounts/settings)

## Run Sample Build

* Clone the repo
* Open the solution `NunitCsharpReqnrollPlaywrightBrowserstack.sln` in Visual Studio (or your IDE of choice)
* Build the solution (`dotnet build`)
* Replace the `userName` and `accessKey` placeholders in `browserstack.yml` with your [BrowserStack Username and Access Key](https://www.browserstack.com/accounts/settings). Alternatively, remove those two lines from the yml and set `BROWSERSTACK_USERNAME` and `BROWSERSTACK_ACCESS_KEY` as environment variables -- the SDK falls back to env vars only when the yml fields are absent

### Running your tests from CLI

```sh
cd NunitCsharpReqnrollPlaywrightBrowserstack.Tests
dotnet test
```

The sample runs across both platforms declared in `browserstack.yml` (Windows 11 / Chrome and macOS / WebKit) in parallel.

Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github).

### Testing a private host (BrowserStack Local)

Use this when the site under test is on `localhost`, a staging host, or behind a firewall. BrowserStack Local opens a secure tunnel and resolves `bs-local.com` back to your machine.

The bundled `LocalSample` scenario navigates to `http://bs-local.com:45454/` and asserts the page title contains `BrowserStack Local` — so serve a matching page on port `45454` first (any page whose `<title>` contains "BrowserStack Local"):

```sh
mkdir -p bs-local-site && printf '<!doctype html><title>BrowserStack Local</title><body>OK</body>' > bs-local-site/index.html
(cd bs-local-site && python3 -m http.server 45454) &
```

To test your own app instead, serve it on port `45454` (or change the port and the title assertion in `StepDefinitions/LocalSampleSteps.cs`). Then set `browserstackLocal: true` in `browserstack.yml` and rerun `dotnet test`. The SDK starts and stops the tunnel for you -- no manual binary download or lifecycle management; it routes `bs-local.com:45454` to your machine's `localhost:45454`.

## Integrate your test suite

This repository uses the BrowserStack SDK to run tests on BrowserStack. To wire the SDK into your own test suite:

* Create a `browserstack.yml` at the project root with your BrowserStack credentials and platform list (see this repo for a working template)
* Add the `BrowserStack.TestAdapter` NuGet package:

  ```sh
  dotnet add package BrowserStack.TestAdapter
  ```

* Build the project (`dotnet build`); the SDK installs the `browserstack-sdk` dotnet tool and patches the test assembly so Playwright launches are routed to BrowserStack at runtime

## How the SDK changes things

- **One `browserstack.yml`** declares platforms, parallelism, the Local toggle, and reporting; the SDK picks them up automatically
- **The SDK runs platforms in parallel for you** -- one NUnit run per `(platform x parallelsPerPlatform)` cell, no per-platform branching needed
- **The SDK rewrites Playwright launches** -- `Hooks/PlaywrightHooks.cs` calls `pw.Chromium.LaunchAsync()` and the SDK transparently redirects to the per-platform browser configured in the yml (`chrome` / `playwright-webkit` / `playwright-firefox` / etc.). No `Chromium.ConnectAsync(wss_url)` plumbing
- **The SDK starts and stops BrowserStack Local** when `browserstackLocal: true` -- no manual tunnel lifecycle management
- **Reqnroll generates NUnit test classes** from `.feature` files at build time, so behaviour-driven scenarios run as standard NUnit tests under `dotnet test`

## Repo layout

```
.
├── NunitCsharpReqnrollPlaywrightBrowserstack.sln
└── NunitCsharpReqnrollPlaywrightBrowserstack.Tests/
    ├── NunitCsharpReqnrollPlaywrightBrowserstack.Tests.csproj
    ├── browserstack.yml          # SDK config: credentials, platforms, Local toggle, reporting
    ├── Features/
    │   ├── Sample.feature        # bstackdemo add-to-cart scenario
    │   └── LocalSample.feature   # BrowserStack Local scenario
    ├── StepDefinitions/
    │   ├── SampleSteps.cs
    │   └── LocalSampleSteps.cs
    └── Hooks/
        └── PlaywrightHooks.cs    # creates IPage per scenario; SDK routes the launch
```

## Notes

* You can view your test results on the [BrowserStack Automate dashboard](https://www.browserstack.com/automate)
* To test on a different set of browsers, see our [list of supported browsers and platforms](https://www.browserstack.com/list-of-browsers-and-platforms?product=automate)
* You can export the environment variables for the Username and Access Key of your BrowserStack account:

  * For Unix-like or Mac machines:
  ```sh
  export BROWSERSTACK_USERNAME=<browserstack-username> &&
  export BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```
  * For Windows Cmd:
  ```cmd
  set BROWSERSTACK_USERNAME=<browserstack-username>
  set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```
  * For Windows Powershell:
  ```powershell
  $env:BROWSERSTACK_USERNAME=<browserstack-username>
  $env:BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

## Further Reading

- [NUnit](https://nunit.org/)
- [Reqnroll](https://reqnroll.net/)
- [Playwright .NET](https://playwright.dev/dotnet/)
- [BrowserStack documentation for Playwright in C#](https://www.browserstack.com/docs/automate/playwright/getting-started/c-sharp)
- [BrowserStack.TestAdapter on NuGet](https://www.nuget.org/packages/BrowserStack.TestAdapter)
- [NUnit reference sample](https://github.com/browserstack/csharp-playwright-browserstack)
- [xUnit + Reqnroll reference sample](https://github.com/browserstack/xunit-reqnroll-playwright-browserstack)

Happy Testing!

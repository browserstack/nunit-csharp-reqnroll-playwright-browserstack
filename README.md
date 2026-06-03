# Reqnroll + NUnit + Playwright with BrowserStack

Sample repository demonstrating how to run C# Reqnroll (NUnit runner) + Playwright tests on
BrowserStack Automate using the [BrowserStack C# SDK](https://www.browserstack.com/docs/automate/selenium/sdk-overview).

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 6.0 or higher (8.0 recommended)
- A [BrowserStack account](https://www.browserstack.com/users/sign_up) (username + access key)
- Git

## Setup

1. Clone this repository and switch to the project directory:

   ```bash
   git clone https://github.com/browserstack/nunit-csharp-reqnroll-playwright-browserstack.git
   cd nunit-csharp-reqnroll-playwright-browserstack/Reqnroll-Playwright-BrowserStack
   ```

2. Restore the BrowserStack SDK tool and project dependencies:

   ```bash
   dotnet tool restore
   dotnet restore
   ```

3. Set your BrowserStack credentials as environment variables:

   ```bash
   export BROWSERSTACK_USERNAME="YOUR_USERNAME"
   export BROWSERSTACK_ACCESS_KEY="YOUR_ACCESS_KEY"
   ```

   Alternatively, edit `browserstack.yml` and set the `userName` and `accessKey` values directly.

## Run Sample Test

Runs the BStack Demo "add to cart" scenario against the platforms configured in `browserstack.yml`:

```bash
dotnet test
```

To run only the sample (non-local) scenario, filter by its tag:

```bash
dotnet test --filter "TestCategory=sample-test"
```

## Run Local Test

Runs the BrowserStack Local scenario (`browserstackLocal: true` is already enabled in `browserstack.yml`):

```bash
dotnet test --filter "TestCategory=sample-local-test"
```

## Notes

- View your test results on the [BrowserStack Automate dashboard](https://automate.browserstack.com/).
- The framework is detected as `nunit` because Reqnroll runs on the NUnit test runner.
- Test Observability is enabled by default (`testObservability: true` in `browserstack.yml`).

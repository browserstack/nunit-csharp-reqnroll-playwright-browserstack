@sample-local-test
Feature: BStack Local
	Scenario: Open BrowserStack Local
		Given I navigate to the local website
		Then the page title should be "BrowserStack Local"

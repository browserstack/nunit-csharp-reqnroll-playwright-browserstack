@sample-test
Feature: BStack Sample
	Scenario: Can add product to cart
		Given I navigate to the StackDemo website
		When I add the first product to the cart
		And the cart pane is opened
		Then the product in the cart matches the product on the page

Feature: Vehicle Configurator

Scenario: User selects and configures an SUV
    Given I navigate to the automotive portal
    When I choose to configure an SUV model
    Then I should see the configuration page
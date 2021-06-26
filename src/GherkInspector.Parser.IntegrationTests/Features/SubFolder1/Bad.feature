Feature: Bad

A short summary of the feature

Scenario: Multiple step keywords
    Given a step
    When another step
    Given another given step
    Then the last step

    Scenario: Scenario indented by 4 spaces
    Given a step
    And another step
    When action
    Then assertion

	Scenario: Scenario indented a tab
    Given a step
    And another step
    When action
    Then assertion

Scenario: A step indented by 5 spaces
    Given a step
     And another step
    When action
    Then assertion

Scenario: A step indented with a tab
	Given a step
    And another step
    When action
    Then assertion
Feature: Wrong Keyword Usage

A short summary of the feature

Scenario: Multiple Given steps
    Given a step
    When another step
    Given another given step
    Then the last step

Scenario: Multiple When steps
    Given a step
    When another step
    When another given step
    Then the last step

Scenario: Multiple Then steps
    Given a step
    When another step
    Then another given step
    Then the last step

Scenario Outline: Scenario outline with a single example should be a Scenario
    Given a step
    When another step
    Then the last step
    Examples: 
        | Value1 | Value2 |
        | 1      | "a"    |
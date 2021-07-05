Feature: Wrong Indentation

Examples of indentation mistakes

    Scenario: Scenario indented
    Given a
    When b
    Then c

Scenario: Steps not indented by 4 spaces
Given a
 And indented with a single space
     When indented with 5 spaces
	Then indented with a tab

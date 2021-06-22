@Sale
Feature: Feature1

# Some comments

@Smoke
Scenario: First Test
    Given a merchant like "test-merchant"
    And some more setup
    And a step that takes a table argument
        | Type   | CVV |
        | "Visa" | 123 |
    When I perform a sale
    Then it was "Approved"
    And a receipt was printed

Scenario Outline: Second Test
    Given a merchant like <Merchant>
    And some more setup
    And a step that takes a table argument
        | Type       | CVV   |
        | <CardType> | <Cvv> |
    When I perform a sale
    Then it was <Result>
    And a receipt was printed
    Examples:
    | Merchant        | CardType       | Cvv | Result     |
    | "test-merchant" | "Visa"         | 123 | "Approved" |
    | "bad-merchant"  | "MasterCard"   | 666 | "Declined" |
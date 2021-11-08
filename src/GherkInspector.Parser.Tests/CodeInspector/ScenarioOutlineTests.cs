namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    using System.Linq;
    using GherkInspector.Parser.CodeInspector;
    using NUnit.Framework;

    [TestFixture]
    public class ScenarioOutlineTests
    {
        private readonly GherkinParser _gherkinParser;

        public ScenarioOutlineTests()
        {
            _gherkinParser = new GherkinParser();
        }

        [Test]
        public void ScenarioOutline_WithOneExample_ShouldBeAScenario()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText($@"
Feature: Example

Scenario Outline: Example
    Given a step
    When a step
    Then a step
    Examples:
        | Value1 |
        | 1      |
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.Fail("Not implemented yet.");
        }
    }
}

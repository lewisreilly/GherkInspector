namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    using System.Linq;
    using GherkInspector.Parser.CodeInspector;
    using NUnit.Framework;

    [TestFixture]
    public class InspectorTests
    {
        [Test]
        public void InspectScenario_FindsWarnings_HasWarningsIsTrue()
        {
            // Arrange
            var inspector = new Inspector();
            var gherkinParser = new GherkinParser();

            var result = gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Bad scenario
    Given a step
    Given another step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
        }
    }
}

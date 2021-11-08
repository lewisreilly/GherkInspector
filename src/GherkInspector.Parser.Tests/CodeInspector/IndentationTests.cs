namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    using System.Linq;
    using GherkInspector.Parser.CodeInspector;
    using NUnit.Framework;

    [TestFixture]
    public class IndentationTests
    {
        private const string Tab = "	";
        private const string ThreeSpaces = "   ";
        private const string FiveSpaces = "     ";

        private readonly GherkinParser _gherkinParser;

        public IndentationTests()
        {
            _gherkinParser = new GherkinParser();
        }

        [Test]
        public void Scenario_IsIndented_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

 Scenario: Example
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.StartWith("5:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Scenario' should not be indented"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Column 2"));
        }

        [TestCase(ThreeSpaces)]
        [TestCase(FiveSpaces)]
        [TestCase(Tab)]
        public void StepIndentation_NotFourSpaces_Warning(string indentation)
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText($@"
Feature: Example

Scenario: Example
{indentation}Given a step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.StartWith("4:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Steps should be indented with 4 spaces"));
        }
    }
}

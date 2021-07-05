namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    using System.Linq;
    using GherkInspector.Parser.CodeInspector;
    using NUnit.Framework;

    [TestFixture]
    public class InspectorTests
    {
        private const string Tab = "	";
        private const string ThreeSpaces = "   ";
        private const string FiveSpaces = "     ";

        private readonly GherkinParser _gherkinParser;

        public InspectorTests()
        {
            _gherkinParser = new GherkinParser();
        }

        [Test]
        public void Inspect_MultipleGivenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Given a step
    Given another step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.StartWith("1:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Given' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 6"));
        }

        [Test]
        public void Inspect_MultipleWhenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    When a step
    When another step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.StartWith("2:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'When' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 6"));
        }

        [Test]
        public void Inspect_MultipleThenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Then a step
    Then another step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.StartWith("3:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Then' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 6"));
        }

        [Test]
        public void Inspect_MultipleViolations_MultipleWarnings()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Given a step
    Given another step
    When a step
    When another step
    Then a step
    Then another step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(3));
            Assert.That(inspector.Warnings[0].Error, Does.StartWith("1:"));
            Assert.That(inspector.Warnings[1].Error, Does.StartWith("2:"));
            Assert.That(inspector.Warnings[2].Error, Does.StartWith("3:"));
        }

        [Test]
        public void Scenario_AnyIndentation_Warning()
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

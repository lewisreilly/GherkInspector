namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    using System.Linq;
    using GherkInspector.Parser.CodeInspector;
    using NUnit.Framework;

    [TestFixture]
    public class KeywordUsageTests
    {
        private readonly GherkinParser _gherkinParser;

        public KeywordUsageTests()
        {
            _gherkinParser = new GherkinParser();
        }

        [Test]
        public void InspectScenario_MultipleGivenSteps_Warning()
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
        public void InspectScenario_MultipleWhenSteps_Warning()
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
        public void InspectScenario_MultipleThenSteps_Warning()
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
        public void InspectScenario_MultipleViolations_MultipleWarnings()
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
        public void InspectScenario_WhenStepAppearsBeforeGivenStep_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    When a step    
    Given a step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Given' should appear before `When` or `Then`"));
        }

        [Test]
        public void InspectScenario_ThenStepAppearsBeforeGivenStep_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Then a step    
    Given a step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Given' should appear before `When` or `Then`"));
        }

        [Test]
        public void InspectScenario_GivenWhenThenInWrongOrder_MultipleWarnings()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Then a step  
    When a step
    Given a step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(2));
            Assert.That(inspector.Warnings[0].Error, Does.Contain("Keyword 'Given' should appear before `When` or `Then`"));
            Assert.That(inspector.Warnings[1].Error, Does.Contain("Keyword 'When' should appear before `Then`"));
        }

        [Test]
        public void InspectScenario_ThenStepAppearsBeforeWhenStep_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Example
    Then a step  
    When a step
");

            // Act
            inspector.InspectScenario(result.Scenarios.First());

            // Assert
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'When' should appear before `Then`"));
        }
    }
}

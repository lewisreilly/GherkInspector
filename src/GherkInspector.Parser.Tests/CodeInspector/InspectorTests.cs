using GherkInspector.Parser.CodeInspector;
using GherkInspector.Parser.Entity;
using NUnit.Framework;
using System.Linq;

namespace GherkInspector.Parser.UnitTests.CodeInspector
{
    [TestFixture]
    public class InspectorTests
    {
        private readonly XLocation Line1 = new XLocation(1, 0);
        private readonly XLocation Line2 = new XLocation(2, 0);

        [Test]
        public void Inspect_MultipleGivenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var scenario = new XScenario();
            scenario.Steps.Add(new XStep("Given ", "a step", Line1));
            scenario.Steps.Add(new XStep("Given ", "another step", Line2));

            // Act
            inspector.InspectScenario(scenario);

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("1:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Given' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 2"));
        }

        [Test]
        public void Inspect_MultipleWhenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var scenario = new XScenario();
            scenario.Steps.Add(new XStep("When ", "a step", Line1));
            scenario.Steps.Add(new XStep("When ", "another step", Line2));

            // Act
            inspector.InspectScenario(scenario);

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("2:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'When' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 2"));
        }

        [Test]
        public void Inspect_MultipleThenSteps_Warning()
        {
            // Arrange
            var inspector = new Inspector();

            var scenario = new XScenario();
            scenario.Steps.Add(new XStep("Then ", "a step", Line1));
            scenario.Steps.Add(new XStep("Then ", "another step", Line2));

            // Act
            inspector.InspectScenario(scenario);

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(1));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("3:"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Keyword 'Then' should only appear once per scenario"));
            Assert.That(inspector.Warnings.First().Error, Does.Contain("Line 2"));
        }

        [Test]
        public void Inspect_MultipleViolations_MultipleWarnings()
        {
            // Arrange
            var inspector = new Inspector();

            var scenario = new XScenario();
            scenario.Steps.Add(new XStep("Given ", "a step", Line1));
            scenario.Steps.Add(new XStep("Given ", "another step", Line2));
            scenario.Steps.Add(new XStep("When ", "a step", new XLocation(3, 0)));
            scenario.Steps.Add(new XStep("When ", "another step", new XLocation(4, 0)));
            scenario.Steps.Add(new XStep("Then ", "a step", new XLocation(5, 0)));
            scenario.Steps.Add(new XStep("Then ", "another step", new XLocation(6, 0)));

            // Act
            inspector.InspectScenario(scenario);

            // Assert
            Assert.That(inspector.HasWarnings, Is.True);
            Assert.That(inspector.Warnings.Count, Is.EqualTo(3));
            Assert.That(inspector.Warnings[0].Error, Does.Contain("1:"));
            Assert.That(inspector.Warnings[1].Error, Does.Contain("2:"));
            Assert.That(inspector.Warnings[2].Error, Does.Contain("3:"));
        }
    }
}

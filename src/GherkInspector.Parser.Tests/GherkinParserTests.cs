using NUnit.Framework;
using System;
using System.Linq;

namespace GherkInspector.Parser.Tests
{
    [TestFixture]
    public class GherkinParserTests
    {
        private readonly GherkinParser _gherkinParser;

        public GherkinParserTests()
        {
            _gherkinParser = new GherkinParser();
        }

        [Test]
        public void Feature_Empty_DefaultValuesAreAsExpected()
        {
            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example
");

            Assert.That(result.Name, Is.EqualTo("Example"));

            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags.Count, Is.EqualTo(0));

            Assert.That(result.Description, Is.EqualTo(string.Empty));

            Assert.That(result.Scenarios, Is.Not.Null);
            Assert.That(result.Scenarios.Count, Is.EqualTo(0));
        }

        [Test]
        public void Feature_HasTags_Parsed()
        {
            var result = _gherkinParser.ParseFeatureText(@"
@FeatureTag
@AnotherTag
Feature: Example
");

            Assert.That(result.Tags.Count, Is.EqualTo(2));
            Assert.That(result.Tags[0], Is.EqualTo("@FeatureTag"));
            Assert.That(result.Tags[1], Is.EqualTo("@AnotherTag"));
        }

        [Test]
        public void Feature_HasDescription_Parsed()
        {
            var result = _gherkinParser.ParseFeatureText(@"
@FeatureTag
Feature: Example

As a user
In order to be able to do X
I want to be able to do Y
");

            Assert.That(result.Description, Is.EqualTo("As a user\r\nIn order to be able to do X\r\nI want to be able to do Y"));
        }

        [Test]
        public void Scenario_Simple_DefaultValuesAreAsExpected()
        {
            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Test One
");

            Assert.That(result.Scenarios.Count, Is.EqualTo(1));
            var scenario = result.Scenarios.First();
            Assert.That(scenario.Name, Is.EqualTo("Test One"));

            Assert.That(scenario.Tags.Count, Is.EqualTo(0));
            Assert.That(scenario.Steps.Count, Is.EqualTo(0));
        }

        [Test]
        public void Scenario_HasTags_Parsed()
        {
            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

@ScenarioTag
@AnotherTag
Scenario: Test One
");

            Assert.That(result.Scenarios.First().Tags[0], Is.EqualTo("@ScenarioTag"));
            Assert.That(result.Scenarios.First().Tags[1], Is.EqualTo("@AnotherTag"));
        }

        [Test]
        public void Scenario_TagAtFeatureLevel_TagIsInherited()
        {
            var result = _gherkinParser.ParseFeatureText(@"
@FeatureTag
Feature: Example

@ScenarioTag
Scenario: Test One
");

            Assert.That(result.Scenarios.First().Tags[0], Is.EqualTo("@FeatureTag"));
            Assert.That(result.Scenarios.First().Tags[1], Is.EqualTo("@ScenarioTag"));
        }

        [Test]
        public void Scenario_HasSteps_Parsed()
        {
            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Test One
    Given a
    When b
    Then c
");
            var scenario = result.Scenarios.First();
            Assert.That(scenario.Steps.Count, Is.EqualTo(3));

            Assert.That(scenario.Steps[0].Keyword, Is.EqualTo("Given "));
            Assert.That(scenario.Steps[0].Text, Is.EqualTo("a"));

            Assert.That(scenario.Steps[1].Keyword, Is.EqualTo("When "));
            Assert.That(scenario.Steps[1].Text, Is.EqualTo("b"));

            Assert.That(scenario.Steps[2].Keyword, Is.EqualTo("Then "));
            Assert.That(scenario.Steps[2].Text, Is.EqualTo("c"));
        }

        [Test]
        public void Scenario_AdditionalStepKeywords_Parsed()
        {
            var result = _gherkinParser.ParseFeatureText(@"
Feature: Example

Scenario: Test One
    Given a
    * a2
    When b
    And b2
    Then c
    But c2
");
            var scenario = result.Scenarios.First();
            Assert.That(scenario.Steps.Count, Is.EqualTo(6));

            Assert.That(scenario.Steps[1].Keyword, Is.EqualTo("* "));
            Assert.That(scenario.Steps[1].Text, Is.EqualTo("a2"));

            Assert.That(scenario.Steps[3].Keyword, Is.EqualTo("And "));
            Assert.That(scenario.Steps[3].Text, Is.EqualTo("b2"));

            Assert.That(scenario.Steps[5].Keyword, Is.EqualTo("But "));
            Assert.That(scenario.Steps[5].Text, Is.EqualTo("c2"));
        }
    }
}

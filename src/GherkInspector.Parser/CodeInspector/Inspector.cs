namespace GherkInspector.Parser.CodeInspector
{
    using System.Collections.Generic;
    using System.Linq;
    using GherkInspector.Parser.Entity;

    public class Inspector
    {
        public bool HasWarnings => Warnings.Count > 0;

        public List<Warning> Warnings { get; private set; }

        public Inspector()
        {
            Warnings = new List<Warning>();
        }

        public void InspectScenario(GherkInspectorScenario scenario)
        {
            ScenarioKeywordShouldNotBeIndented(scenario);
            GivenKeywordShouldOnlyBeMentionedOnce(scenario);
            WhenKeywordShouldOnlyBeMentionedOnce(scenario);
            ThenKeywordShouldOnlyBeMentionedOnce(scenario);

            StepKeywordsShouldBeIndentedWith4Spaces(scenario);
            GivenWhenThenKeywordsShouldAppearInTheCorrectOrder(scenario);
        }

        private void ScenarioKeywordShouldNotBeIndented(GherkInspectorScenario scenario)
        {
            if (scenario.Location.Column > 1)
            {
                Warnings.Add(
                    new Warning(
                        "5",
                        scenario.Location,
                        "Keyword 'Scenario' should not be indented"));
            }
        }

        private void GivenKeywordShouldOnlyBeMentionedOnce(GherkInspectorScenario scenario)
        {
            if (scenario.Steps.Count(step => step.Keyword == "Given ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "1",
                        badStep.Location,
                        "Keyword 'Given' should only appear once per scenario"));
            }
        }

        private void WhenKeywordShouldOnlyBeMentionedOnce(GherkInspectorScenario scenario)
        {
            if (scenario.Steps.Count(step => step.Keyword == "When ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "2",
                        badStep.Location,
                        "Keyword 'When' should only appear once per scenario"));
            }
        }

        private void ThenKeywordShouldOnlyBeMentionedOnce(GherkInspectorScenario scenario)
        {
            if (scenario.Steps.Count(step => step.Keyword == "Then ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "3",
                        badStep.Location,
                        "Keyword 'Then' should only appear once per scenario"));
            }
        }

        private void StepKeywordsShouldBeIndentedWith4Spaces(GherkInspectorScenario scenario)
        {
            foreach (var step in scenario.Steps)
            {
                if (step.Location.Column != 5)
                {
                    Warnings.Add(
                    new Warning(
                        "4",
                        step.Location,
                        "Steps should be indented with 4 spaces"));
                }
            }
        }

        private void GivenWhenThenKeywordsShouldAppearInTheCorrectOrder(GherkInspectorScenario scenario)
        {
            var firstGivenStep = scenario.Steps.Where(step => step.Keyword == "Given ").FirstOrDefault();
            var firstWhenStep = scenario.Steps.Where(step => step.Keyword == "When ").FirstOrDefault();
            var firstThenStep = scenario.Steps.Where(step => step.Keyword == "Then ").FirstOrDefault();

            if (firstGivenStep != null && (firstWhenStep != null || firstThenStep != null))
            {
                var firstNonGivenStep = firstWhenStep?.Location.Line ?? firstThenStep.Location.Line;

                if (firstGivenStep.Location.Line > firstNonGivenStep)
                {
                    Warnings.Add(
                        new Warning(
                            "0",
                            scenario.Location,
                            "Keyword 'Given' should appear before `When` or `Then`"));
                }
            }

            if (firstWhenStep != null && firstThenStep != null)
            {
                if (firstWhenStep.Location.Line > firstThenStep.Location.Line)
                {
                    Warnings.Add(
                        new Warning(
                            "0",
                            scenario.Location,
                            "Keyword 'When' should appear before `Then`"));
                }
            }
        }
    }
}

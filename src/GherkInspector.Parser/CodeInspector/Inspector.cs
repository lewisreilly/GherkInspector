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
            if (scenario.Location.Column > 1)
            {
                Warnings.Add(
                    new Warning(
                        "5",
                        scenario.Location,
                        "Keyword 'Scenario' should not be indented"));
            }

            if (scenario.Steps.Count(step => step.Keyword == "Given ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "1",
                        badStep.Location,
                        "Keyword 'Given' should only appear once per scenario"));
            }

            if (scenario.Steps.Count(step => step.Keyword == "When ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "2",
                        badStep.Location,
                        "Keyword 'When' should only appear once per scenario"));
            }

            if (scenario.Steps.Count(step => step.Keyword == "Then ") > 1)
            {
                var badStep = scenario.Steps.Last();
                Warnings.Add(
                    new Warning(
                        "3",
                        badStep.Location,
                        "Keyword 'Then' should only appear once per scenario"));
            }

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
    }
}

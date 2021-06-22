using Gherkin.Ast;
using GherkInspector.Parser.Entity;
using System.IO;

namespace GherkInspector.Parser
{
    public class GherkinParser
    {
        public XFeature ParseFeatureText(string text)
        {
            GherkinDocument gherkinDocument;
            var parser = new Gherkin.Parser();
            using (var reader = new StringReader(text))
            {
                gherkinDocument = parser.Parse(reader);
            }

            var feature = new XFeature();
            feature.Name = gherkinDocument.Feature.Name;
            
            ParseDescription(gherkinDocument, feature);

            foreach (var tag in gherkinDocument.Feature.Tags)
            {
                feature.Tags.Add(tag.Name);
            }

            foreach (var featureChild in gherkinDocument.Feature.Children)
            {
                var xScenario = new XScenario();

                if (featureChild is Scenario scenario)
                {
                    var xLocation = new XLocation(scenario.Location.Line, scenario.Location.Column);
                    xScenario = new XScenario(scenario.Name, xLocation);

                    foreach (var tag in feature.Tags)
                    {
                        xScenario.Tags.Add(tag);
                    }

                    foreach (var tag in scenario.Tags)
                    {
                        xScenario.Tags.Add(tag.Name);
                    }
                }

                if (featureChild is StepsContainer stepsContainer)
                {
                    foreach (var step in stepsContainer.Steps)
                    {
                        xScenario.Steps.Add(
                            new XStep(
                                step.Keyword,
                                step.Text,
                                new XLocation(
                                    step.Location.Line,
                                    step.Location.Column)
                                )
                            );
                    }
                }

                feature.Scenarios.Add(xScenario);
            }

            return feature;
        }

        private void ParseDescription(GherkinDocument gherkinDocument, XFeature feature)
        {
            if (gherkinDocument.Feature.Description != null)
            {
                feature.Description = gherkinDocument.Feature.Description;
            }
        }
    }
}

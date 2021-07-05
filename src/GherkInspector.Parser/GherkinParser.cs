using Gherkin.Ast;
using GherkInspector.Parser.Entity;
using System;
using System.IO;
using System.Linq;

namespace GherkInspector.Parser
{
    public class GherkinParser
    {
        public GherkInspectorFeature ParseFeatureText(string text)
        {
            GherkinDocument gherkinDocument;
            var parser = new Gherkin.Parser();
            using (var reader = new StringReader(text))
            {
                try
                {
                    gherkinDocument = parser.Parse(reader);
                }
                catch (Exception)
                {
                    return new GherkInspectorFeature();
                }
            }

            var feature = new GherkInspectorFeature();
            feature.Name = gherkinDocument.Feature.Name;

            ParseDescription(gherkinDocument, feature);

            foreach (var tag in gherkinDocument.Feature.Tags)
            {
                feature.Tags.Add(tag.Name);
            }

            foreach (var featureChild in gherkinDocument.Feature.Children)
            {
                var xScenario = new GherkInspectorScenario();

                if (featureChild is Scenario scenario)
                {
                    var xLocation = new GherkInspectorLocation(scenario.Location.Line, scenario.Location.Column);
                    xScenario = new GherkInspectorScenario(scenario.Name, xLocation);

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
                            new GherkInspectorStep(
                                step.Keyword,
                                step.Text,
                                new GherkInspectorLocation(
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

        private void ParseDescription(GherkinDocument gherkinDocument, GherkInspectorFeature feature)
        {
            if (gherkinDocument.Feature.Description != null)
            {
                feature.Description = gherkinDocument.Feature.Description;
            }
        }
    }
}

namespace GherkInspector.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Gherkin.Ast;
    using GherkInspector.Parser.Entity;

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
                var gherkInspectorScenario = new GherkInspectorScenario();

                if (featureChild is Scenario scenario)
                {
                    gherkInspectorScenario = new GherkInspectorScenario(scenario.Name, ConvertLocation(scenario.Location));

                    foreach (var tag in feature.Tags)
                    {
                        gherkInspectorScenario.Tags.Add(tag);
                    }

                    foreach (var tag in scenario.Tags)
                    {
                        gherkInspectorScenario.Tags.Add(tag.Name);
                    }

                    foreach (var example in scenario.Examples)
                    {
                        var tableHeaderCells = new List<GherkInspectorTableCell>();
                        foreach (var cell in example.TableHeader.Cells)
                        {
                            tableHeaderCells.Add(
                                new GherkInspectorTableCell(
                                    ConvertLocation(cell.Location),
                                    cell.Value));
                        }

                        var headerRow = new GherkInspectorTableRow(
                            ConvertLocation(example.TableHeader.Location),
                            tableHeaderCells);

                        var x = new GherkInspectorExample(
                            ConvertLocation(example.Location),
                            headerRow);

                        gherkInspectorScenario.Examples.Add(x);
                    }
                }

                if (featureChild is StepsContainer stepsContainer)
                {
                    foreach (var step in stepsContainer.Steps)
                    {
                        gherkInspectorScenario.Steps.Add(
                            new GherkInspectorStep(
                                step.Keyword,
                                step.Text,
                                ConvertLocation(step.Location)
                                )
                            );
                    }
                }

                feature.Scenarios.Add(gherkInspectorScenario);
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

        private GherkInspectorLocation ConvertLocation(Location location)
        {
            return new GherkInspectorLocation(location.Line, location.Column);
        }
    }
}

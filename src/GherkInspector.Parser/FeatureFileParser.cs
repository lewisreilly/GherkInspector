using Gherkin.Ast;
using GherkInspector.Parser.Model;

namespace GherkInspector.Parser
{
    public class FeatureFileParser
    {
        public GherkInspectorFeature Parse(string gherkinText)
        {
            GherkinDocument gherkinDocument;
            var parser = new Gherkin.Parser();

            using (var reader = new StringReader(gherkinText))
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

            return feature;
        }
    }
}

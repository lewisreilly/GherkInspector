namespace GherkInspector.Parser.Entity
{
    using System.Collections.Generic;
    using System.Linq;

    public class ParserResults
    {
        public int TotalFeatureCount => Features.Count;

        public int TotalScenarioCount => Features.Sum(f => f.Scenarios.Count);

        public ParserResults(List<GherkInspectorFeature> features)
        {
            Features = features;
        }

        public List<GherkInspectorFeature> Features { get; private set; }
    }
}

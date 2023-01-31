using GherkInspector.Parser.Model;

namespace GherkInspector.Parser
{
    public class FeatureFileFinderResult
    {
        public int TotalFeatureCount => Features.Count;

        public List<GherkInspectorFeature> Features { get; private set; }

        public FeatureFileFinderResult(List<GherkInspectorFeature> features)
        {
            Features = features;
        }        
    }
}

using System.Collections.Generic;

namespace GherkInspector.Parser.Entity
{
    public class GherkInspectorFeature
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public List<GherkInspectorScenario> Scenarios { get; set; }
        public string Path { get; set; }

        public GherkInspectorFeature()
        {
            Tags = new List<string>();
            Description = string.Empty;
            Scenarios = new List<GherkInspectorScenario>();
        }
    }
}

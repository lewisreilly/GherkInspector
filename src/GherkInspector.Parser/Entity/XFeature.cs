using System.Collections.Generic;

namespace GherkInspector.Parser.Entity
{
    public class XFeature
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public List<XScenario> Scenarios { get; set; }
        public string Path { get; set; }

        public XFeature()
        {
            Tags = new List<string>();
            Description = string.Empty;
            Scenarios = new List<XScenario>();
        }
    }
}

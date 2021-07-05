namespace GherkInspector.Parser.Entity
{
    using System.Collections.Generic;

    public class GherkInspectorScenario
    {
        public string Name { get; private set; }

        public GherkInspectorLocation Location { get; private set; }

        public List<string> Tags { get; private set; }

        public List<GherkInspectorStep> Steps { get; private set; }

        public GherkInspectorScenario()
            : this(string.Empty, new GherkInspectorLocation(0, 0))
        {
        }

        public GherkInspectorScenario(string name, GherkInspectorLocation location)
        {
            Name = name;
            Location = location;

            Tags = new List<string>();
            Steps = new List<GherkInspectorStep>();
        }
    }
}

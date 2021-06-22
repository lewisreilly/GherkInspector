using System.Collections.Generic;

namespace GherkInspector.Parser.Entity
{
    public class XScenario
    {
        public string Name { get; private set; }
        public XLocation Location { get; private set; }
        public List<string> Tags { get; private set; }
        public List<XStep> Steps { get; private set; }

        public XScenario()
            : this(string.Empty, new XLocation(0, 0))
        {
        }

        public XScenario(string name, XLocation location)
        {
            Name = name;
            Location = location;

            Tags = new List<string>();
            Steps = new List<XStep>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GherkInspector.Parser.Entity
{
    public class Overview
    {
        public int TotalFeatureCount => Features.Count;

        public int TotalScenarioCount => Features.Sum(f => f.Scenarios.Count);

        public Overview(List<XFeature> features)
        {
            Features = features;
        }

        public List<XFeature> Features { get; private set; }
    }
}

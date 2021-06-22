using GherkInspector.Parser.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GherkInspector.Parser
{
    public class FeatureFileReader
    {
        private readonly GherkinParser _gherkinParser;

        public FeatureFileReader()
        {
            _gherkinParser = new GherkinParser();
        }

        public Overview Read(string path)
        {
            var files = Directory.GetFiles(path, "*.feature", SearchOption.AllDirectories);

            var featureFiles = new List<XFeature>();
            foreach (string filename in files)
            {
                var feature = ParseFeatureFile(filename);

                var fileInfo = new FileInfo(filename);
                feature.Path = fileInfo.Directory.FullName;

                featureFiles.Add(feature);
            }

            return new Overview(featureFiles);
        }

        private XFeature ParseFeatureFile(string filePath)
        {
            string fileContents;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                fileContents = streamReader.ReadToEnd();
            }

            return _gherkinParser.ParseFeatureText(fileContents);
        }
    }
}

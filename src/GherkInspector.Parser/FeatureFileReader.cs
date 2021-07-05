namespace GherkInspector.Parser
{
    using System.Collections.Generic;
    using System.IO;
    using GherkInspector.Parser.Entity;

    public class FeatureFileReader
    {
        private readonly GherkinParser _gherkinParser;

        public FeatureFileReader()
        {
            _gherkinParser = new GherkinParser();
        }

        public ParserResults Read(string path)
        {
            var files = Directory.GetFiles(path, "*.feature", SearchOption.AllDirectories);

            var featureFiles = new List<GherkInspectorFeature>();
            foreach (string filename in files)
            {
                var feature = ParseFeatureFile(filename);

                var fileInfo = new FileInfo(filename);
                feature.Path = fileInfo.Directory.FullName;

                featureFiles.Add(feature);
            }

            return new ParserResults(featureFiles);
        }

        private GherkInspectorFeature ParseFeatureFile(string filePath)
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

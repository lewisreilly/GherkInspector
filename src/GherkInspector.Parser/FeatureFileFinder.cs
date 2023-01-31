using GherkInspector.Parser.Model;

namespace GherkInspector.Parser
{
    public class FeatureFileFinder
    {
        private readonly FeatureFileParser FeatureFileParser;

        public FeatureFileFinder()
        {
            FeatureFileParser = new FeatureFileParser();
        }
        
        public List<GherkInspectorFeature> Find(string startingPath)
        {
            var filenames = Directory.GetFiles(startingPath, "*.feature", SearchOption.AllDirectories);

            var featureFiles = new List<GherkInspectorFeature>();
            foreach (var filename in filenames)
            {
                var feature = ParseFeatureFile(filename);

                var fileInfo = new FileInfo(filename);
                feature.Path = fileInfo.Directory.FullName;

                featureFiles.Add(feature);
            }

            return featureFiles;
        }

        private GherkInspectorFeature ParseFeatureFile(string filePath)
        {
            string fileContents;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                fileContents = streamReader.ReadToEnd();
            }

            return FeatureFileParser.Parse(fileContents);
        }
    }
}
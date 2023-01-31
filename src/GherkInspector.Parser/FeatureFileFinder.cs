namespace GherkInspector.Parser
{
    public class FeatureFileFinder
    {
        public List<string> Find(string startingPath)
        {
            var files = Directory.GetFiles(startingPath, "*.feature", SearchOption.AllDirectories);
            return files.ToList();
        }
    }
}
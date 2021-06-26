namespace GherkInspector.Parser.Entity
{
    public class GherkInspectorStep
    {
        public string Keyword { get; private set; }

        public string Text { get; private set; }

        public GherkInspectorLocation Location { get; private set; }

        public GherkInspectorStep(string keyword, string text, GherkInspectorLocation location)
        {
            Keyword = keyword;
            Text = text;
            Location = location;
        }
    }
}

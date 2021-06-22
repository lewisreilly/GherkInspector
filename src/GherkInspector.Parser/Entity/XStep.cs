namespace GherkInspector.Parser.Entity
{
    public class XStep
    {
        public string Keyword { get; private set; }

        public string Text { get; private set; }

        public XLocation Location { get; private set; }

        public XStep(string keyword, string text, XLocation location)
        {
            Keyword = keyword;
            Text = text;
            Location = location;
        }
    }
}

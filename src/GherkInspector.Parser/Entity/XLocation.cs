namespace GherkInspector.Parser.Entity
{
    public class XLocation
    {
        public XLocation(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }
    }
}

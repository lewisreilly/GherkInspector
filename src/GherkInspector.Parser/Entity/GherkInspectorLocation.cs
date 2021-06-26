namespace GherkInspector.Parser.Entity
{
    public class GherkInspectorLocation
    {
        public GherkInspectorLocation(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }
    }
}

namespace GherkInspector.Parser.CodeInspector
{
    using GherkInspector.Parser.Entity;

    public class Warning
    {
        private string _id;
        private GherkInspectorLocation _location;
        private string _message;

        public string Error => $"{_id}: {_message}. Line {_location.Line}, Column {_location.Column}.";

        public Warning(string id, GherkInspectorLocation location, string message)
        {
            _id = id;
            _location = location;
            _message = message;
        }
    }
}

using GherkInspector.Parser.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GherkInspector.Parser.CodeInspector
{
    public class Warning
    {
        private string _id;
        private XLocation _location;
        private string _message;

        public string Error => $"{_id}: {_message}. Line {_location.Line}, Column {_location.Column}.";

        public Warning(string id, XLocation location, string message)
        {
            _id = id;
            _location = location;
            _message = message;
        }
    }
}

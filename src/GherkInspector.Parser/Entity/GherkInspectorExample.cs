namespace GherkInspector.Parser.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GherkInspectorExample
    {
        public GherkInspectorLocation Location { get; private set; }
        public GherkInspectorTableRow TableHeader { get; private set; }
        public IEnumerable<GherkInspectorTableRow> TableBody { get; private set; }

        public GherkInspectorExample(GherkInspectorLocation location, GherkInspectorTableRow headerRow)
        {
            Location = location;
            TableHeader = headerRow;
            TableBody = new List<GherkInspectorTableRow>();
        }
    }

    public class GherkInspectorTableRow
    {
        public GherkInspectorLocation Location { get; private set; }
        public IEnumerable<GherkInspectorTableCell> Cells { get; private set; }

        public GherkInspectorTableRow(GherkInspectorLocation location, List<GherkInspectorTableCell> cells)
        {
            Location = location;
            Cells = cells;
        }
    }

    public class GherkInspectorTableCell
    {
        public GherkInspectorLocation Location { get; private set; }
        public string Value { get; private set; }

        public GherkInspectorTableCell(GherkInspectorLocation location, string value)
        {
            Location = location;
            Value = value;
        }
    }

    /*
     * example
{Gherkin.Ast.Examples}
    Description: null
    Keyword: "Examples"
    Location: {Gherkin.Ast.Location}
    Name: ""
    TableBody: {Gherkin.Ast.TableRow[1]}
    TableHeader: {Gherkin.Ast.TableRow}
    Tags: {Gherkin.Ast.Tag[0]}
example.TableHeader
{Gherkin.Ast.TableRow}
    Cells: {Gherkin.Ast.TableCell[1]}
    Location: {Gherkin.Ast.Location}
example.TableBody
{Gherkin.Ast.TableRow[1]}
    [0]: {Gherkin.Ast.TableRow}

    */
}

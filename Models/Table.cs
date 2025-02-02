using System.Collections.Generic;

namespace TinySql.Models
{
    public class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}

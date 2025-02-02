using System;
using System.Collections.Generic;
using System.Linq;
using TinySql.Storage;

namespace TinySql.Models
{
    public class Database
    {
        // Singleton instance for simplicity.
        private static Database _instance;
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Load from disk when the instance is first requested.
                    _instance = FileStorage.Load();
                }
                return _instance;
            }
        }

        public List<Table> Tables { get; set; } = new List<Table>();

        public void CreateTable(string tableName, string schema)
        {
            if (Tables.Any(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Table '{tableName}' already exists!");
                return;
            }
            var table = new Table { Name = tableName };

            // Example schema: "id INT, name TEXT"
            var columnDefs = schema.Split(',');
            foreach (var colDef in columnDefs)
            {
                var trimmed = colDef.Trim();
                var parts = trimmed.Split(' ');
                if (parts.Length >= 2)
                {
                    table.Columns.Add(new Column { Name = parts[0], Type = parts[1] });
                }
                else
                {
                    Console.WriteLine($"Invalid column definition: {trimmed}");
                }
            }
            Tables.Add(table);
            Console.WriteLine($"Table '{tableName}' created successfully with {table.Columns.Count} column(s).");
            // Save after table creation.
            FileStorage.Save(this);
        }

        public void InsertInto(string tableName, string values)
        {
            var table = Tables.FirstOrDefault(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                Console.WriteLine($"Table '{tableName}' not found!");
                return;
            }

            // For demonstration, assume values are provided as: <id>, <name>
            var parts = values.Split(',');
            if (parts.Length < 2 || !int.TryParse(parts[0].Trim(), out int id))
            {
                Console.WriteLine("Invalid values provided. Expected format: <id>, <name>");
                return;
            }
            var name = parts[1].Trim().Trim('\''); // Remove any quotes around the name

            var row = new Row { Id = id, Name = name };
            table.Rows.Add(row);
            Console.WriteLine($"Inserted row ({row.Id}, {row.Name}) into table '{tableName}'.");
            // Save after inserting a row.
            FileStorage.Save(this);
        }

        public void SelectAll(string tableName)
        {
            var table = Tables.FirstOrDefault(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                Console.WriteLine($"Table '{tableName}' not found!");
                return;
            }
            Console.WriteLine($"Table: {table.Name}");
            Console.WriteLine("ID | Name");
            Console.WriteLine("------------");
            foreach (var row in table.Rows)
            {
                Console.WriteLine($"{row.Id} | {row.Name}");
            }
        }
    }
}

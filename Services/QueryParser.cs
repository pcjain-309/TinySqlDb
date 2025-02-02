using System;
using TinySql.Models;
using TinySql.Services.Commands;

namespace TinySql.Services
{
    public class QueryParser
    {
        public ICommand Parse(string query)
        {
            // For demonstration, we check for basic commands.
            // In a complete solution, consider using regex or a parser library.
            query = query.Trim();
            if (query.StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
            {
                // Example: CREATE TABLE mytable (id INT, name TEXT)
                // Extract table name and schema between parentheses.
                int startIdx = query.IndexOf(" ", 12) + 1;
                int parenStart = query.IndexOf("(", startIdx);
                int parenEnd = query.IndexOf(")", parenStart);
                if (parenStart < 0 || parenEnd < 0)
                    throw new Exception("Invalid CREATE TABLE syntax.");
                
                string tableName = query.Substring(startIdx, parenStart - startIdx).Trim();
                string schema = query.Substring(parenStart + 1, parenEnd - parenStart - 1).Trim();
                return new CreateTableCommand(tableName, schema);
            }
            else if (query.StartsWith("INSERT INTO", StringComparison.OrdinalIgnoreCase))
            {
                // Example: INSERT INTO mytable VALUES (1, 'JohnDoe')
                int tableNameStart = "INSERT INTO".Length;
                int valuesIndex = query.IndexOf("VALUES", StringComparison.OrdinalIgnoreCase);
                if (valuesIndex < 0)
                    throw new Exception("Invalid INSERT syntax.");
                
                string tableName = query.Substring(tableNameStart, valuesIndex - tableNameStart).Trim();
                int parenStart = query.IndexOf("(", valuesIndex);
                int parenEnd = query.IndexOf(")", parenStart);
                if (parenStart < 0 || parenEnd < 0)
                    throw new Exception("Invalid VALUES syntax.");
                
                string values = query.Substring(parenStart + 1, parenEnd - parenStart - 1).Trim();
                return new InsertCommand(tableName, values);
            }
            else if (query.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                // Example: SELECT * FROM mytable
                string[] tokens = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 4)
                    throw new Exception("Invalid SELECT syntax.");
                string tableName = tokens[3];
                return new SelectCommand(tableName);
            }
            else
            {
                throw new Exception("Unknown SQL command.");
            }
        }
    }
}

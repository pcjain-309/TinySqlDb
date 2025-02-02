using System;
using TinySql.Models;

namespace TinySql.Services.Commands
{
    public class InsertCommand : ICommand
    {
        private readonly string _tableName;
        private readonly string _values;
        
        public InsertCommand(string tableName, string values)
        {
            _tableName = tableName;
            _values = values;
        }
        
        public void Execute()
        {
            Database.Instance.InsertInto(_tableName, _values);
        }
    }
}

using System;
using TinySql.Models;

namespace TinySql.Services.Commands
{
    public class CreateTableCommand : ICommand
    {
        private readonly string _tableName;
        private readonly string _schema;
        
        public CreateTableCommand(string tableName, string schema)
        {
            _tableName = tableName;
            _schema = schema;
        }
        
        public void Execute()
        {
            // For now, we call a static method on Database. Later, consider dependency injection.
            Database.Instance.CreateTable(_tableName, _schema);
        }
    }
}

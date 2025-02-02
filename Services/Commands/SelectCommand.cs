using System;
using TinySql.Models;

namespace TinySql.Services.Commands
{
    public class SelectCommand : ICommand
    {
        private readonly string _tableName;
        
        public SelectCommand(string tableName)
        {
            _tableName = tableName;
        }
        
        public void Execute()
        {
            Database.Instance.SelectAll(_tableName);
        }
    }
}

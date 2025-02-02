using System;
using TinySql.Models;
using TinySql.Services;

namespace TinySql
{
    class Program
    {
        static void Main(string[] args)
        {
            // Optionally save database on process exit.
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Storage.FileStorage.Save(Database.Instance);
            };

            Console.WriteLine("Welcome to TinySql CLI");
            Console.WriteLine("Type your SQL query or 'EXIT' to quit.");

            var parser = new QueryParser();
            while (true)
            {
                Console.Write("SQL> ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    break;

                try
                {
                    var command = parser.Parse(input);
                    command.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TinySql.Models;

namespace TinySql.Storage
{
    public static class FileStorage
    {
        // File to store our database (you can change the path as needed)
        private static readonly string StorageFile = "database.json";

        /// <summary>
        /// Saves the current database state to disk.
        /// </summary>
        public static void Save(Database db)
        {
            try
            {
                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    // In case you have cyclic references or want to ignore nulls, adjust here.
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                string json = JsonSerializer.Serialize(db, options);
                File.WriteAllText(StorageFile, json);
                Console.WriteLine("Database saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving database: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads the database state from disk.
        /// </summary>
        public static Database Load()
        {
            try
            {
                if (!File.Exists(StorageFile))
                {
                    Console.WriteLine("No saved database found. Starting with a new one.");
                    return new Database();
                }

                string json = File.ReadAllText(StorageFile);
                var db = JsonSerializer.Deserialize<Database>(json);
                Console.WriteLine("Database loaded successfully.");
                return db ?? new Database();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading database: {ex.Message}");
                return new Database();
            }
        }
    }
}

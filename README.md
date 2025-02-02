# TinySql CLI

TinySql CLI is a lightweight, terminal-based SQL-like database management system built with C# and .NET. It allows users to interact with a simple in-memory database using basic SQL commands and provides JSON-based persistent storage, making it an excellent project for learning database fundamentals and command-line application development.

## Features

- **Command-Line Interface:** Interact with TinySql using an intuitive terminal-based CLI.
- **SQL-like Commands:** Supports a subset of SQL commands:
  - `CREATE TABLE` – Create a new table with a specified schema.
  - `INSERT INTO` – Insert data into an existing table.
  - `SELECT * FROM` – Retrieve and display all data from a table.
  - `EXIT` – Exit the CLI.
- **Persistent Storage:** Uses JSON serialization to save the database state to disk, ensuring your data persists across sessions.
- **Modular Design:** Organized into Models, Services, and Storage components for easy maintenance and extension.
- **Cross-Platform:** Developed using .NET, making it runnable on any platform that supports the .NET runtime (including Ubuntu WSL).

## Project Structure

The TinySql project is structured in a modular way to ensure maintainability and scalability. Below is a breakdown of the project directories and files:

````markdown
TinySql/
├── Program.cs
├── Models/
│   ├── Database.cs
│   ├── Table.cs
│   ├── Row.cs
│   └── Column.cs
├── Services/
│   ├── QueryParser.cs
│   └── Commands/
│       ├── ICommand.cs
│       ├── CreateTableCommand.cs
│       ├── InsertCommand.cs
│       └── SelectCommand.cs
└── Storage/
    └── FileStorage.cs
````
---

## 📂 **Directory Breakdown**

### **1. Root Directory (`TinySql/`)**
- Contains the main entry point of the application (`Program.cs`).

### **2. `Program.cs`**
- The main entry point of the CLI application.
- Initializes the database and starts the command-line loop.
- Handles user input and executes SQL-like commands.

---

### **3. Models (`Models/`)**
This directory contains classes representing the core database structure.

- **`Database.cs`** → Manages tables and provides methods for CRUD operations.
- **`Table.cs`** → Represents an individual database table with columns and rows.
- **`Row.cs`** → Defines the structure of a row in a table.
- **`Column.cs`** → Defines the properties of a table column (e.g., name, data type).

---

### **4. Services (`Services/`)**
This directory contains services responsible for SQL parsing and command execution.

- **`QueryParser.cs`** → Parses user-input SQL commands and converts them into executable actions.
  
#### 📂 **Commands (`Commands/`)**
Contains command implementations for executing SQL-like statements.

- **`ICommand.cs`** → Defines an interface for all SQL-like commands.
- **`CreateTableCommand.cs`** → Implements the `CREATE TABLE` command.
- **`InsertCommand.cs`** → Implements the `INSERT INTO` command.
- **`SelectCommand.cs`** → Implements the `SELECT * FROM` command.

---

### **5. Storage (`Storage/`)**
This directory handles data persistence.

- **`FileStorage.cs`** →  Saves and loads the database from a JSON file (database.json) to ensure persistence across sessions.
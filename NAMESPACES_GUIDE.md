# Understanding Namespaces and Using Statements in C#

This guide explains how namespaces and using statements work in C#, using examples from your TodoApi project.

---

## What is a Namespace?

Think of a **namespace** as a container or folder that organizes your code. It's like putting items in labeled boxes so you know where to find them.

**Real-world analogy:**

- If you have a toolbox, the namespace is like the label on the toolbox
- Inside are different tools (your classes)
- You might have a "Power Tools" box and a "Hand Tools" box - these are different namespaces

### Why Use Namespaces?

1. **Prevents naming conflicts** - Two classes can have the same name if they're in different namespaces
2. **Organizes code** - Groups related classes together
3. **Makes code readable** - Shows where code belongs

---

## How Namespaces Work in Your Project

Let's look at your actual files:

### Example 1: The Todo Model

```csharp
// File: Models/Todo.cs
namespace TodoApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}
```

**What this means:**

- The `Todo` class lives inside the `TodoApi.Models` namespace
- To use this class elsewhere, you need to know its full "address": `TodoApi.Models.Todo`
- It's like an address: `TodoApi.Models` is the street, `Todo` is the house

### Your Project's Namespace Structure

Here are all the namespaces in your project:

```
TodoApi.Models          → Contains: Todo
TodoApi.Data            → Contains: TodoDb
TodoApi.Services        → Contains: TodoService
TodoApi.Controllers     → Contains: TodoController
TodoApi.DTO             → Contains: TodoItemDto, CreateTodoItemDto, UpdateTodoItemDto
ITodoService            → Contains: ITodoService interface
ITodoRepository         → Contains: ITodoRepository interface
Repositories            → Contains: TodoRepository
```

---

## What are Using Statements?

**Using statements** are like telling the compiler: *"Hey, when I write `Todo`, I mean `TodoApi.Models.Todo`"*

They allow you to use short names instead of the full namespace path.

### Example 2: Without Using Statement (Verbose)

```csharp
// This is VERBOSE - you must use the full name every time
var todo = new TodoApi.Models.Todo();
var dto = new TodoApi.DTO.TodoItemDto();
```

### Example 3: With Using Statement (Clean)

```csharp
// At the top of the file
using TodoApi.Models;
using TodoApi.DTO;

// Now you can use short names
var todo = new Todo();
var dto = new TodoItemDto();
```

---

## Real Examples from Your Code

### Example 4: Program.cs

```csharp
// These lines tell C#: "I'll be using classes from these namespaces"
using Microsoft.EntityFrameworkCore;  // External library for database
using TodoApi.Data;                    // Your TodoDb class
using TodoApi.Services;                // Your TodoService class
using TodoApi.Controllers;             // Your TodoController class

// Later in the code, you can write:
builder.Services.AddDbContext<TodoDb>(...);  // Instead of TodoApi.Data.TodoDb
```

**Breakdown:**

- Line 1: `using Microsoft.EntityFrameworkCore;`
  - This lets you use `DbContext`, `DbSet`, etc. from the Entity Framework library
  - Without it, you'd write `Microsoft.EntityFrameworkCore.DbContext`
  
- Line 2: `using TodoApi.Data;`
  - This lets you use `TodoDb` class directly
  - The `TodoDb` class is defined in the `TodoApi.Data` namespace

### Example 5: TodoService.cs

```csharp
// File: Services/TodoService.cs
using TodoApi.DTO;        // For TodoItemDto, CreateTodoItemDto, UpdateTodoItemDto
using TodoApi.Models;     // For Todo
using ITodoRepository = ITodoRepository.ITodoRepository;
using ITodoService = ITodoService.ITodoService;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        // Inside here, you can use:
        // - TodoItemDto (from TodoApi.DTO)
        // - Todo (from TodoApi.Models)
        // - ITodoRepository (aliased)
    }
}
```

**Why these using statements?**

- `using TodoApi.DTO;` → Lets you use `TodoItemDto`, `CreateTodoItemDto`, etc. without the full name
- `using TodoApi.Models;` → Lets you use `Todo` class
- The last two are **aliases** (explained below)

---

## Special Case: Aliases (Renaming Namespaces)

Sometimes you have naming conflicts or want shorter names. That's what aliases are for.

### Example 6: Using Aliases in Program.cs

```csharp
using ITodoRepository = ITodoRepository.ITodoRepository;
using ITodoService = ITodoService.ITodoService;
```

**What this does:**

- These interfaces are in namespaces that share the same name as the interface itself
- `ITodoService.ITodoService` means: "The namespace is `ITodoService` AND the interface is also named `ITodoService`"
- The alias says: "When I write `ITodoService`, I mean `ITodoService.ITodoService`"

**Full form vs Alias:**

```csharp
// Without alias (verbose):
ITodoService.ITodoService service = ...;

// With alias (clean):
ITodoService service = ...;
```

---

## Namespace Declaration Syntax

There are two ways to declare a namespace:

### Method 1: Traditional (with braces)

```csharp
namespace TodoApi.Services
{
    public class TodoService
    {
        // Code here
    }
}
```

### Method 2: File-scoped (modern, no braces)

```csharp
namespace TodoApi.Models;  // Note the semicolon, not brace

public class Todo
{
    // Code here - everything in this file is in TodoApi.Models
}
```

**Your project uses both styles** - both are valid! The file-scoped style is newer and cleaner for single-namespace files.

---

## Rules and Best Practices

### Rule 1: Using Statements Go at the Top

```csharp
// ✅ CORRECT
using System;
using TodoApi.Models;

namespace MyProject;

public class MyClass { }
```

```csharp
// ❌ WRONG
namespace MyProject;

using System;  // Error! Using must come before namespace
```

### Rule 2: Order Matters for Clarity (but not functionally)

```csharp
// Typical order:
// 1. External libraries (System, Microsoft, etc.)
// 2. Your own project namespaces
// 3. Aliases (if any)

using System;                    // External
using Microsoft.EntityFrameworkCore;  // External
using TodoApi.Models;            // Your project
using TodoApi.DTO;               // Your project
using ITodoService = ITodoService.ITodoService;  // Alias
```

### Rule 3: Namespace Matches Folder Structure (Convention)

```csharp
// File location: Models/Todo.cs
namespace TodoApi.Models;  // Matches the folder structure

// File location: Services/TodoService.cs
namespace TodoApi.Services;  // Matches the folder structure
```

This makes it easier to find files!

### Rule 4: You Can Use Fully Qualified Names

```csharp
// Even without using statements, you can always use the full name:
var todo = new TodoApi.Models.Todo();
```

---

## Common Mistakes to Avoid

### Mistake 1: Forgetting Using Statement

```csharp
// ❌ This won't compile - Todo is not recognized
namespace MyProject;

public class MyClass
{
    public void DoSomething()
    {
        var todo = new Todo();  // Error! What is Todo?
    }
}
```

**Fix:**

```csharp
// ✅ Add the using statement
using TodoApi.Models;

namespace MyProject;

public class MyClass
{
    public void DoSomething()
    {
        var todo = new Todo();  // Now it works!
    }
}
```

### Mistake 2: Wrong Namespace

```csharp
// ❌ Wrong namespace
namespace WrongNamespace;

public class TodoService  // This won't match your using statements
```

**Fix:**

```csharp
// ✅ Correct namespace
namespace TodoApi.Services;
```

---

## How It All Works Together: A Complete Example

Let's trace through how `TodoController.cs` uses namespaces:

```csharp
// File: Controllers/TodoController.cs

// 1. Using statements - "I need these namespaces"
using TodoApi.DTO;              // For CreateTodoItemDto, UpdateTodoItemDto, TodoItemDto
using ITodoService = ITodoService.ITodoService;  // For ITodoService interface

// 2. Declare this file's namespace
namespace TodoApi.Controllers;

// 3. Now define the class in that namespace
public static class TodoController
{
    // Inside here, you can use:
    // - CreateTodoItemDto (because of "using TodoApi.DTO")
    // - ITodoService (because of the alias)
    // - WebApplication (from ASP.NET Core, automatically available)
    
    public static void MapTodoController(this WebApplication app)
    {
        app.MapPost("/todoitems", async (CreateTodoItemDto dto, ITodoService service) =>
        {
            // CreateTodoItemDto works because of "using TodoApi.DTO"
            // ITodoService works because of the alias
            var created = await service.CreateAsync(dto);
            return Results.Created($"/todoitems/{created.id}", created);
        });
    }
}
```

---

## Summary: The Key Concepts

1. **Namespace** = A container/label for organizing classes
   - Example: `TodoApi.Models` contains the `Todo` class

2. **Using Statement** = Tells C# "I'll be using classes from this namespace"
   - Allows you to write `Todo` instead of `TodoApi.Models.Todo`

3. **Namespace Declaration** = Says "This file's code belongs to this namespace"
   - Example: `namespace TodoApi.Controllers;`

4. **Full Qualified Name** = Always works, even without using statements
   - Example: `TodoApi.Models.Todo`

5. **Aliases** = Rename namespaces for convenience
   - Example: `using ITodoService = ITodoService.ITodoService;`

---

## Quick Reference: Your Project Structure

```
TodoApi/
├── Models/
│   └── Todo.cs                    → namespace TodoApi.Models
├── Data/
│   └── TodoDb.cs                  → namespace TodoApi.Data
├── Services/
│   ├── TodoService.cs             → namespace TodoApi.Services
│   └── Interfaces/
│       └── ITodoService.cs        → namespace ITodoService
├── Repositories/
│   ├── TodoRepository.cs          → namespace Repositories
│   └── Interfaces/
│       └── ITodoRepository.cs     → namespace ITodoRepository
├── Controllers/
│   └── TodoController.cs          → namespace TodoApi.Controllers
├── DTOs/
│   ├── TodoItemDto.cs             → namespace TodoApi.DTO
│   ├── CreateTodoItemDto.cs       → namespace TodoApi.DTO
│   └── UpdateTodoItemDto.cs       → namespace TodoApi.DTO
└── Program.cs                     → (no namespace - uses using statements)
```

---

## Practice Exercise

Try to identify what's happening in `Program.cs`:

```csharp
using TodoApi.Data;          // Q: What does this allow?
using TodoApi.Services;      // Q: What does this allow?
using TodoApi.Controllers;   // Q: What does this allow?

// Later in the code:
builder.Services.AddDbContext<TodoDb>(...);     // Q: Which namespace is TodoDb from?
builder.Services.AddScoped<ITodoService, TodoService>();  // Q: Which namespaces?
app.MapTodoController();     // Q: Which namespace is MapTodoController from?
```

**Answers:**

- `using TodoApi.Data;` allows you to use `TodoDb` directly
- `TodoDb` is from `TodoApi.Data` namespace
- `TodoService` is from `TodoApi.Services` namespace
- `MapTodoController` is from `TodoApi.Controllers` namespace

---

I hope this helps! Namespaces are just organizational tools - like folders on your computer, but for code!

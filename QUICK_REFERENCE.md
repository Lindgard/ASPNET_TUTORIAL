# Quick Reference: Namespaces & Using Statements

## The Simplest Explanation

**Namespace** = A label that groups related code together  
**Using Statement** = "I'll be using code from this namespace, so I don't have to type the full path"

---

## Visual Analogy

```
📦 TodoApi.Models
   └── Todo.cs (contains: class Todo)

📦 TodoApi.DTO  
   └── TodoItemDto.cs (contains: class TodoItemDto)
```

When you write `using TodoApi.Models;`, you're saying:
- "Open the TodoApi.Models box"
- "Now I can use `Todo` directly instead of `TodoApi.Models.Todo`"

---

## Two-Part System

### Part 1: Declare Namespace (in the file that defines the class)

```csharp
// File: Models/Todo.cs
namespace TodoApi.Models;  // "This file belongs to TodoApi.Models"

public class Todo { ... }
```

### Part 2: Import Namespace (in the file that uses the class)

```csharp
// File: Services/TodoService.cs
using TodoApi.Models;  // "I want to use classes from TodoApi.Models"

public class TodoService
{
    public void DoSomething()
    {
        var todo = new Todo();  // Works because of the using statement!
    }
}
```

---

## Real Example from Your Code

### Step-by-Step: How TodoService uses Todo

1. **Todo is defined** in `Models/Todo.cs`:
   ```csharp
   namespace TodoApi.Models;  // Todo lives here
   
   public class Todo { ... }
   ```

2. **TodoService imports it** in `Services/TodoService.cs`:
   ```csharp
   using TodoApi.Models;  // "Bring Todo into scope"
   
   namespace TodoApi.Services;
   
   public class TodoService
   {
       public TodoItemDto CreateAsync(...)
       {
           var todo = new Todo();  // ✅ Works! C# knows where to find Todo
           // ...
       }
   }
   ```

3. **Without the using statement**, you'd have to write:
   ```csharp
   var todo = new TodoApi.Models.Todo();  // Verbose!
   ```

---

## Common Patterns in Your Project

### Pattern 1: Simple Using Statement
```csharp
using TodoApi.Models;      // Use all classes from TodoApi.Models
using TodoApi.DTO;         // Use all classes from TodoApi.DTO
```

### Pattern 2: Alias (when namespace and class have same name)
```csharp
using ITodoService = ITodoService.ITodoService;
//                   └─ namespace.ClassName
```

**Why?** Because the namespace is `ITodoService` and the interface is also named `ITodoService`:
- Full name: `ITodoService.ITodoService`
- Alias allows: `ITodoService`

### Pattern 3: External Libraries
```csharp
using Microsoft.EntityFrameworkCore;  // From NuGet package, not your project
```

---

## File Structure = Namespace Structure (Usually)

```
Folder Structure           →    Namespace
─────────────────────────────────────────────
Models/Todo.cs            →    TodoApi.Models
Services/TodoService.cs   →    TodoApi.Services
Data/TodoDb.cs            →    TodoApi.Data
DTOs/TodoItemDto.cs       →    TodoApi.DTO
```

This is a **convention** (best practice), not a requirement. But it makes code easier to find!

---

## The Flow in Program.cs

Look at your `Program.cs`:

```csharp
// STEP 1: Import namespaces
using TodoApi.Data;        // Now I can use TodoDb
using TodoApi.Services;    // Now I can use TodoService  
using TodoApi.Controllers; // Now I can use TodoController

// STEP 2: Use the classes (short names work!)
builder.Services.AddDbContext<TodoDb>(...);
//                        ^^^^^^^ Works because of "using TodoApi.Data"

builder.Services.AddScoped<ITodoService, TodoService>();
//                                   ^^^^^^^^^^ Works because of "using TodoApi.Services"

app.MapTodoController();
//   ^^^^^^^^^^^^^^^^^ Works because of "using TodoApi.Controllers"
```

---

## Troubleshooting

### Problem: "The type or namespace name 'Todo' could not be found"

**Solution:** Add `using TodoApi.Models;` at the top of your file

### Problem: "Ambiguous reference between X and Y"

**Solution:** Use fully qualified name or an alias:
```csharp
var todo = new TodoApi.Models.Todo();  // Full name
// OR
using MyTodo = TodoApi.Models.Todo;
var todo = new MyTodo();
```

---

## Remember

1. ✅ **Using statements** go at the **top** of the file
2. ✅ **Namespace declarations** come after using statements
3. ✅ One namespace per file (usually)
4. ✅ Using statements make code shorter and cleaner

---

**Think of it like this:**  
- **Namespace** = The address of your class  
- **Using statement** = Adding that address to your "contacts" so you can call it by its short name


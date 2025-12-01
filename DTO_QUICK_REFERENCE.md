# DTOs - Quick Reference

## What is a DTO?

**DTO = Data Transfer Object**

A "package" that carries data between your application and the outside world.

---

## The Simple Concept

```
Your Internal Code          DTOs           Outside World
(Todo Model)         ◄─── Package ───►    (API Clients)
```

- **Model** = How you store data internally (database)
- **DTO** = How you send/receive data to/from clients

---

## Your Three DTOs

### 1️⃣ `CreateTodoItemDto` - Creating a Todo
```csharp
public class CreateTodoItemDto
{
    public string Name { get; set; } = string.Empty;
    // NO Id (database generates it)
    // NO IsComplete (defaults to false)
}
```
**When:** Client creates a new todo  
**Contains:** Just the Name

### 2️⃣ `UpdateTodoItemDto` - Updating a Todo
```csharp
public class UpdateTodoItemDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    // NO Id (comes from URL)
}
```
**When:** Client updates an existing todo  
**Contains:** Name and IsComplete

### 3️⃣ `TodoItemDto` - Reading Todos
```csharp
public class TodoItemDto
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}
```
**When:** API sends todos back to client  
**Contains:** Everything (Id, Name, IsComplete)

---

## Visual Flow Example

### Creating a Todo:

```
1. Client sends:
   {
       "Name": "Buy milk"
   }
   ↓
2. Received as CreateTodoItemDto
   ↓
3. Converted to Todo Model
   (Id auto-generated, IsComplete = false)
   ↓
4. Saved to Database
   ↓
5. Converted back to TodoItemDto
   {
       "id": 1,
       "Name": "Buy milk",
       "IsComplete": false
   }
   ↓
6. Sent back to Client
```

---

## Why Use DTOs?

### ✅ Security
- Hide internal fields
- Prevent clients from manipulating database IDs

### ✅ Flexibility  
- Change your database structure without breaking your API
- Different data shapes for different operations

### ✅ Clean API Design
- Show only what clients need
- Better control over your API interface

---

## Model vs DTO

| Model (`Todo`) | DTO (`TodoItemDto`, etc.) |
|---------------|---------------------------|
| Internal structure | External interface |
| Matches database | Matches API |
| Full details | Only what's needed |
| Used inside app | Used for API communication |

---

## The Conversion

Your service converts between Model and DTO:

```csharp
// Model → DTO
TodoItemDto MapToDto(Todo todo)
{
    return new TodoItemDto
    {
        id = todo.Id,
        Name = todo.Name,
        IsComplete = todo.IsComplete
    };
}

// DTO → Model (when creating)
Todo todo = new Todo { Name = dto.Name };
// Id and IsComplete are set automatically
```

---

## Think of It Like This

**Model** = Your private journal (all details)  
**DTO** = A postcard (only what others need to see)

Or:

**Model** = The chef's full recipe (internal)  
**DTO** = The menu description (customer-facing)

---

## Quick Examples from Your Code

### Example 1: Creating a Todo
```csharp
// Client sends CreateTodoItemDto
POST /todoitems
{
    "Name": "Learn C#"
}

// Service converts to Todo Model
// Database saves it
// Service converts to TodoItemDto
// Client receives TodoItemDto
```

### Example 2: Getting All Todos
```csharp
// Client requests
GET /todoitems

// Service gets List<Todo> from database
// Service converts to List<TodoItemDto>
// Client receives List<TodoItemDto>
```

### Example 3: Updating a Todo
```csharp
// Client sends UpdateTodoItemDto
PUT /todoitems/5
{
    "Name": "Learn C# - DONE!",
    "IsComplete": true
}

// Service finds Todo with Id=5
// Service updates Todo from UpdateTodoItemDto
// Saves to database
```

---

## Remember

1. **Model** = Internal (database structure)
2. **DTO** = External (API structure)  
3. **Conversion** = Always happens between Model and DTO
4. **Different DTOs** = For different operations (create, update, read)

---

**TL;DR:** DTOs are like "masks" that control what data goes in and out of your API. They protect your internal structure and give you flexibility!


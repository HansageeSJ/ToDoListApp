using System.Collections.Concurrent; // Use ConcurrentDictionary to store key/value pairs in memory that helps handle multiple requests at the same time. Thread-safe dictionary
using ToDoListApp.Models; //Use this, because service deals with Model class.


namespace ToDoListApp.Services; //Use to keep the service related code together.

public class ToDoListService : IToDoListService // This shows that Interface class implements through ToDoListService class.
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _items = new(); //_item is the in-memory database. Key=Guid, Value=TodoItem. Since we dont have a real database, everything keeps in here.

    public IEnumerable<TodoItem> GetAll() => _items.Values.OrderBy(i => i.Title); //Returns all the saved _items values in alphabetically order.

    public TodoItem? Get(Guid id) => _items.TryGetValue(id, out var item) ? item : null; //Check the ToDoList items by the id, if the key exists terun the item, else null.

    public TodoItem Add(string title)
    {
        var item = new TodoItem(Guid.NewGuid(), title.Trim(), false); // Generate unique ID then remove the extra spaces and the new task is not done yet so keep it as false.
        _items[item.Id] = item; //Save the item in _items in-memory DB
        return item; //returns the new item.
    }

    public bool Update(Guid id, bool isDone)
    {
        var exisiting = Get(id); // Check the todo item by its ID
        if (exisiting is null) return false; // if its not exists then return false
        _items[id] = exisiting with { IsDone = isDone }; //if its found then use the "with" expression to copy the item but update its IsDone value and save it back to _items.
        return true;
    }

    public bool Delete(Guid id) => _items.TryRemove(id, out _); //Delete the ToDoList item for the given id. If item deletes successfully then returns True. If not False.

    public void Clear() => _items.Clear(); // Use to remove all the items from the memory. Use only for unit tetsting.
    
}
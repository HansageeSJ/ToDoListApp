/*
Interface is a blueprint that defines what operations will happen on the ToDOListApp.
*/

using ToDoListApp.Models; // Can use the classes from the Models folder

/* Groups the interface inside the Services namespace. 
   NameSpaces usefull to organize the code that helps not to mix Models, Controllers and Services
*/


namespace ToDoListApp.Services;

public interface IToDoListService
{
    IEnumerable<TodoItem> GetAll(); //Returns the list of all the todo items. 
    TodoItem? Get(Guid id); //Fetch single todo item by its unique id. And also can returns null, if no item with taht ID exists.
    TodoItem Add(string title); // Add a new todo item with given title.
    bool Update(Guid id, bool isDone); //Since this is bool, return values are either true or false. Update the status of exisiting todo item and return true. If not false, that means id not exists.
    bool Delete(Guid id); // Since this is bool, return values are either true or false. Delete a todo item by its id. True, if delete is succeede, otherwise its false when item not found.
    void Clear(); // add this for testing purposes. 
}

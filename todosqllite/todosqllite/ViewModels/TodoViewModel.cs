using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using todosqllite.Data;
using todosqllite.Models;

namespace todosqllite.ViewModels
{
    public class TodoViewModel
    {
        private TodoItemDatabase database;
        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public TodoViewModel(string dbPath)
        {
            database = new TodoItemDatabase(dbPath);
            TodoItems = new ObservableCollection<TodoItem>();
            InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            var items = await database.GetTodoItemsAsync();
            foreach (var item in items)
            {
                TodoItems.Add(item);
            }
        }

        // Add methods for Add, Complete, Delete operations that interact with the database
        // and update the TodoItems collection.
    }
}

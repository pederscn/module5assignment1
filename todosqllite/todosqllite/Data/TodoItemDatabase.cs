using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using todosqllite.Models;

namespace todosqllite.Data
{
    public class TodoItemDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TodoItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TodoItem>().Wait();
        }

        public Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return _database.Table<TodoItem>().ToListAsync();
        }

        public Task<TodoItem> GetTodoItemAsync(int id)
        {
            return _database.Table<TodoItem>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTodoItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteTodoItemAsync(TodoItem item)
        {
            return _database.DeleteAsync(item);
        }
    }
}


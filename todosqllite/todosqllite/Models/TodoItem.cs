using System;
using SQLite;

namespace todosqllite.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
    }
}

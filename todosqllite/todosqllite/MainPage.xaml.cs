using System;
using System.IO;
using Xamarin.Forms;
using todosqllite.Models;
using todosqllite.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace todosqllite
{
    public partial class MainPage : ContentPage
    {
        private TodoItemDatabase database;
        private List<TodoItem> todoItems;

        public MainPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
            database = new TodoItemDatabase(dbPath);
            RefreshTodoItems().ConfigureAwait(false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshTodoItems();
        }

        private async Task RefreshTodoItems()
        {
            var allItems = await database.GetTodoItemsAsync();
            var activeItems = allItems.Where(x => !x.IsCompleted).ToList();
            var completedItems = allItems.Where(x => x.IsCompleted).ToList();

            TodoItemsListView.ItemsSource = activeItems; // This is your existing ListView
            //CompletedItemsListView.ItemsSource = completedItems; // This is a new ListView for completed items
        }


        private async void OnAddClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Task", "Enter task name:");
            if (!string.IsNullOrEmpty(result))
            {
                var newItem = new TodoItem { TaskName = result, IsCompleted = false };
                await database.SaveTodoItemAsync(newItem); // Updated to correct method name
                await RefreshTodoItems();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is TodoItem item)
            {
                // Navigate to a new page or display details about the selected item
                DisplayAlert("Item Selected", item.TaskName, "OK");
                // Deselect item
                ((ListView)sender).SelectedItem = null;
            }
        }
        private async void OnCompleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.BindingContext as TodoItem;
            item.IsCompleted = !item.IsCompleted;
            await database.SaveTodoItemAsync(item);
            await RefreshTodoItems();
        }
        private async void OnCompleteChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TodoItem todoItem)
            {
                todoItem.IsCompleted = checkBox.IsChecked;
                await database.SaveTodoItemAsync(todoItem);
                RefreshTodoItems();
            }
        }


    }
}

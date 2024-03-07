using System;
using System.IO;
using todosqllite.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todosqllite
{
    public partial class App : Application
    {
        static TodoItemDatabase database;

        // Static property to access the database throughout your app
        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            // Set the start page of your app
            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}


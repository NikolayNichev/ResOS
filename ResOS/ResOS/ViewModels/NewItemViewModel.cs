using ResOS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SQLite;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ResOS.ViewModels
{
    public class NewItemViewModel : INotifyPropertyChanged
    {
        //LETSA FOOKIN GOOO
        private readonly MenuDatabase menuDatabase;
        private SQLiteAsyncConnection connection;
        private DatabaseModel databaseModel;
        private MenuItems menu;
        //

        public ICommand SaveCommand { get; set; }
        public Command CancelCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewItemViewModel()
        {
            databaseModel = new DatabaseModel(); //the database model is created
            connection = databaseModel.GetConnection(); //the connection is retrieved
            connection.CreateTableAsync<MenuItems>(); //a table of MenuItems is created in case one doesn't  
                                                      // already exist 

            menuDatabase = new MenuDatabase(); //the database of menu items is initialized

            menu = new MenuItems(); //the menu object is initialised 
            
            //
            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(OnCancel);
            //this.PropertyChanged +=
            //    (_, __) => SaveCommand.ChangeCanExecute();
            //
        }

        public string Text
        {
            get { return menu.Text; }
            set { menu.Text = value; }
        }

        public string Description
        {
            get { return menu.Description; }
            set { menu.Description = value; }
        }
        public double Price         
        {
            get 
            {
                return menu.Price;
            }
            set 
            {
                menu.Price = value;                
            }
        }



        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async Task OnSave()
        {
            //New
            if (AllFieldsFull())  //the all method is used to check if the user has entered all fields
            {
                await menuDatabase.AddMenuItem(menu); //the menu item is added to the database
                MessagingCenter.Send(this, "MenuItemAdded", menu); //the messaging sender contacts the ItemsPage to add items to the menu through the 
                                                                   //MenuItemAdded method
                await Application.Current.MainPage.Navigation.PopAsync(); //the page closes
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "Please fill all fields to continue", "OK"); //if the user enters the incorrect input an error is displayed
            }


            //OLD
            //Models.MenuItems newItem = new Models.MenuItems()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Text = Text,
            //    Description = Description
            //};

            //await DataStore.AddItemAsync(newItem);

            //// This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }

        //the boolean checks if the user has entered all fields in the program
        public bool AllFieldsFull() 
        {
            if (string.IsNullOrWhiteSpace(menu.Text) || string.IsNullOrWhiteSpace(menu.Description) || double.IsNaN(menu.Quantity))              
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}

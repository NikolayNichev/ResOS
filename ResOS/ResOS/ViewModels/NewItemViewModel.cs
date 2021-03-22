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
            databaseModel = new DatabaseModel();
            connection = databaseModel.GetConnection();
            connection.CreateTableAsync<MenuItems>();

            menuDatabase = new MenuDatabase();

            menu = new MenuItems();
            
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
            if (AllFieldsFull()) 
            {
                await menuDatabase.AddMenuItem(menu);
                MessagingCenter.Send(this, "MenuItemAdded", menu);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "Please fill all fields to continue", "OK");
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

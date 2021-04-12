using ResOS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Windows.Input;

namespace ResOS.ViewModels
{
    class BasketPageViewModel : INotifyPropertyChanged
    {
        ObservableCollection<MenuItems> MenuOrder { get; set; } = new ObservableCollection<MenuItems>();
        private readonly MenuDatabase menuDatabase;
        private readonly DatabaseModel databaseModel;
        private readonly SQLiteAsyncConnection connection;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadOrdersCommand;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BasketPageViewModel()         
        {
            // the messaging center is subscribed to the itemdetailviewmodel which will send items from there which the user has added to the basked
            // and once the items have been added, the OnMenuItemAdded method will be executed
            MessagingCenter.Subscribe<ItemDetailViewModel, MenuItems>(this, "MenuItemAdded", OnMenuItemAdded);
            menuDatabase = new MenuDatabase();

            databaseModel = new DatabaseModel();
            connection = databaseModel.GetConnection();


            LoadOrdersCommand = new Command(async () => await LoadOrders());

        }

        private void OnMenuItemAdded(ItemDetailViewModel arg1, MenuItems menuItem)
        {
            MenuOrder.Add(menuItem);
        }

        async Task LoadOrders() 
        {
            try
            {
                //MenuOrder.Clear();
                var items = await menuDatabase.GetAddedItems();
                foreach (var item in items)
                {
                    MenuOrder.Add(item);
                }
                OnPropertyChanged();                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

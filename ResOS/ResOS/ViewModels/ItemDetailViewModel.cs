using ResOS.Models;
using SQLite;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResOS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : INotifyPropertyChanged
    {
        MenuItems menuItem = new MenuItems();
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        //the menu database will be called to update the items as being added
        private readonly MenuDatabase menuDatabase;
        private readonly DatabaseModel databaseModel;
        private readonly SQLiteAsyncConnection connection;



        public ICommand AddMenuItemCommand { get; set; }
        public ICommand MinusButtonPressedCommand;
        public ICommand PlusButtonPressedCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ItemDetailViewModel(MenuItems newMenuItem) 
        {

            menuDatabase = new MenuDatabase(); //the menu database is initialised

            databaseModel = new DatabaseModel();
            connection = databaseModel.GetConnection();



            AddMenuItemCommand = new Command(async () => await AddToBasket());
            MinusButtonPressedCommand = new Command(async () => await MinusButtonPressed());
            PlusButtonPressedCommand = new Command(async () => await PlusButtonPressed());            


            // since the page will be used for opening current menu items, the current MenuItems object will be 
            //initialised with the newMenuItem paramater if one is passed through the program 
            if (newMenuItem == null)
            {
                menuItem = new MenuItems();
            }
            else 
            {
                menuItem = newMenuItem;
            }
        }


        public string Text
        {
            get { return menuItem.Text; }
            set { menuItem.Text = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return menuItem.Description; }
            set { 
                menuItem.Description = value;
                OnPropertyChanged();
            }
        }

        public string ItemId
        {
            get
            {
                return menuItem.Id;
            }
            set
            {
                menuItem.Id= value;
                OnPropertyChanged();
            }
        }
        public double Price 
        {
            get 
            {
                return menuItem.Price;
            }
            set 
            {
                menuItem.Price = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        
        {
            get 
            {
                return menuItem.Quantity;
            }
            set 
            {
                menuItem.Quantity = value;
                OnPropertyChanged();
            }
        }

        public string PriceInGBP
        {
            get
            {
                return "£" + menuItem.Price.ToString();
            }
        }

        public bool IsAdded 
        {
            get 
            {
                return menuItem.IsAdded;                
            }
            set 
            {
                menuItem.IsAdded = value;
                OnPropertyChanged();
            }
        }

        public async Task AddToBasket() 
        {
            if (Quantity > 0)
            {
                IsAdded = true;
                //await connection.UpdateAsync(menuItem);
                await menuDatabase.UpdateMenuItem(menuItem); //the menu database is updated 
                MessagingCenter.Send(this, "MenuItemAdded", menuItem);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }


        public async Task MinusButtonPressed()
        {
            if (Quantity > 0)
            {
                Quantity--;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public async Task PlusButtonPressed()
        {
            if (Quantity < 10)
            {
                Quantity += 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Quantity));
            }
        }

        //public async void LoadItemId(string itemId)
        //{
        //    try
        //    {
        //        var item = await DataStore.GetItemAsync(itemId);
        //        Id = item.Id;
        //        Text = item.Text;
        //        Description = item.Description;
        //    }
        //    catch (Exception)
        //    {
        //        Debug.WriteLine("Failed to Load Item");
        //    }
        //}

       
    }
}

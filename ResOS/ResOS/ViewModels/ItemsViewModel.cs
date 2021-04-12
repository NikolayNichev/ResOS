using ResOS.Models;
using ResOS.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResOS.ViewModels
{
    public class ItemsViewModel : INotifyPropertyChanged
    {
        private Models.MenuItems _selectedItem;
        private MenuDatabase menuDatabase;

        //the property changed event handler will be used by the view model to notify the view and the model
        //in case something changes within the data and data logic
        public event PropertyChangedEventHandler PropertyChanged;

        //the observable collection will be used to store all menu items 
        public ObservableCollection<ItemDetailViewModel> Items { get; set; } = new ObservableCollection<ItemDetailViewModel>();
        public Command LoadItemsCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand GoToBasketCommand { get; set; }


        //the itemsRetrieved boolean is used so that the items are not retrieved twice. 
        private bool itemsRetrieved;

        public ICommand SelectItemCommand { get; set; }
        public Command<Models.MenuItems> ItemTapped { get; set; }


        //the property changed event handler is used whenever the ViewModel needs to notify the model that something has changed
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ItemsViewModel()
        {
            menuDatabase = new MenuDatabase();
            
            Items = new ObservableCollection<ItemDetailViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(async () =>
            {
                //if the user chooses the add item button, the new item page will be pushed 
                await Application.Current.MainPage.Navigation.PushAsync(new NewItemPage());
                OnPropertyChanged();
            });
            GoToBasketCommand = new Command(async () => await GoToCheckout());


            //since the command requires a parameter, it is initalised through the ItemDetailViewModel
            SelectItemCommand = new Command<ItemDetailViewModel>(async c => await SelectItem(c));

            AddItemCommand = new Command(OnAddItem);

            //the messaging center awaits messages from the new item view model in case it sends a new menu item 
            //which is to be added to the observable collection 
            MessagingCenter.Subscribe<NewItemViewModel, MenuItems>(this, "MenuItemAdded", OnMenuItemAdded);
        }

        async Task ExecuteLoadItemsCommand()
        {
            //if the items have already been retrieved, the database isn't accessed
            if (itemsRetrieved == true)
            {
                return;
            }
            else 
            { 
                //a try catch block is used in case accessing the database throws an exception
            try
            {
                    //if they haven't been retrieved, the database gets all menu items, and each item gets
                    //added to the observable collection
                    //then the property changed event handler is notified
                itemsRetrieved = true;
                Items.Clear();
                var items = await menuDatabase.GetMenuItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(new ItemDetailViewModel(item));
                }
                    OnPropertyChanged();
                OnPropertyChanged(CheckoutItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            }
        }

        
        public string CheckoutItems 
        {
            get 
            {
                string itemString = $"Checkout ({0})";
                return string.Format(itemString, Items.Count.ToString()); 
            }
        }

        

        private async void OnAddItem(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
    

        //this method is used whenever the messaging center in the new item detail page sends 
        //an item to be added to the observable collection
        public void OnMenuItemAdded(NewItemViewModel source, MenuItems items) 
        {
            Items.Add(new ItemDetailViewModel(items));
        }


        //if a user chooses to access a menu item to add to the basked, the AddItemPage is initialised with the MenuItem itself
        //as a parameter 
        public async Task SelectItem(ItemDetailViewModel item) 
        {
            if (item != null) 
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage(item));
            }
        }

        //the basket page is opened when the user presses the button 
        public async Task GoToCheckout()         
        {
            await Application.Current.MainPage.Navigation.PushAsync(new BasketPage());
        }
    }
}
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemDetailViewModel> Items { get; set; } = new ObservableCollection<ItemDetailViewModel>();
        public Command LoadItemsCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand GoToBasketCommand { get; set; }


        private bool itemsRetrieved;

        public ICommand SelectItemCommand { get; set; }
        public Command<Models.MenuItems> ItemTapped { get; set; }


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
                await Application.Current.MainPage.Navigation.PushAsync(new NewItemPage());
                OnPropertyChanged();
            });
            GoToBasketCommand = new Command(async () => await GoToCheckout());



            SelectItemCommand = new Command<ItemDetailViewModel>(async c => await SelectItem(c));

            AddItemCommand = new Command(OnAddItem);

            MessagingCenter.Subscribe<NewItemViewModel, MenuItems>(this, "MenuItemAdded", OnMenuItemAdded);
        }

        async Task ExecuteLoadItemsCommand()
        {

            if (itemsRetrieved == true)
            {
                return;
            }
            else 
            { 
            try
            {
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
    

        public void OnMenuItemAdded(NewItemViewModel source, MenuItems items) 
        {
            Items.Add(new ItemDetailViewModel(items));
        }


        public async Task SelectItem(ItemDetailViewModel item) 
        {
            if (item != null) 
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage(item));
            }
        }

        public async Task GoToCheckout()         
        {
            await Application.Current.MainPage.Navigation.PushAsync(new BasketPage());
        }
    }
}
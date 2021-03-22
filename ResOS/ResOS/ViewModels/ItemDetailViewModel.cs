using ResOS.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResOS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        MenuItems menuItem = new MenuItems();
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }


        public ICommand AddMenuItemCommand;
        public ICommand MinusButtonPressedCommand;
        public ICommand PlusButtonPressedCommand;


        public ItemDetailViewModel(MenuItems newMenuItem) 
        {

            AddMenuItemCommand = new Command(async () => await AddToBasket());
            MinusButtonPressedCommand = new Command(async () => await MinusButtonPressed());
            PlusButtonPressedCommand = new Command(async () => await PlusButtonPressed());


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
            }
        }


        public async Task MinusButtonPressed() 
        {
            if (Quantity > 0) 
            {
                Quantity--;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public async Task PlusButtonPressed() 
        {
            if (Quantity < 10) 
            {
                Quantity++;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

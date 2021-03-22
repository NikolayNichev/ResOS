using ResOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResOS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage(ItemDetailViewModel itemDetailViewModel)
        {
            InitializeComponent();
            ViewModel = itemDetailViewModel;
        }

        ItemDetailViewModel ViewModel
        {
            get { return BindingContext as ItemDetailViewModel; }
            set { BindingContext = value; }
        }

        public void MenuItemSelected(object sender, SelectedItemChangedEventArgs eventArgs) 
        {
            
        }
    }
}
using ResOS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ResOS.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(ItemDetailViewModel itemDetailViewModel)
        {
            InitializeComponent();
            ViewModel = itemDetailViewModel;
        }

        ItemDetailViewModel ViewModel
        {
            get { return BindingContext as ItemDetailViewModel; }
            set { BindingContext = value; }
        }
    }
}
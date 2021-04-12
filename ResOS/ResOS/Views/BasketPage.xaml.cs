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
    public partial class BasketPage : ContentPage
    {
        public BasketPage()
        {
            InitializeComponent();
            ViewModel = new BasketPageViewModel();
        }

        //this is why you document and comment your code so you don't waste
        //previous time
        protected override void OnAppearing()
        {
            ViewModel.LoadOrdersCommand.Execute(null);
            base.OnAppearing();
        }

        BasketPageViewModel ViewModel
        {
            get { return BindingContext as BasketPageViewModel; }
            set { BindingContext = value; }
        }
    }
}
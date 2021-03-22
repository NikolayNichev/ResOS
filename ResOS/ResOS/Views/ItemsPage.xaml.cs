using ResOS.Models;
using ResOS.ViewModels;
using ResOS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResOS.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            ViewModel = new ItemsViewModel();

            //BindingContext = _viewModel = new ItemsViewModel();
        }

        ItemsViewModel ViewModel 
        {
            get { return BindingContext as ItemsViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadItemsCommand.Execute(null);
            base.OnAppearing();
        }

        public void MenuItemSelected(object sender, SelectedItemChangedEventArgs eventArgs) 
        {
            ViewModel.SelectItemCommand.Execute(eventArgs.SelectedItem);
            ListView list = sender as ListView;
            list.SelectedItem = -1;
        }
    }
}
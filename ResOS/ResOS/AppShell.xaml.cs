using ResOS.ViewModels;
using ResOS.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResOS
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void RateMenuItemClicked(object sender, EventArgs e) 
        {
            await Launcher.OpenAsync(new Uri("https://play.google.com/store"));
        }




        private void CloseApplicationItemClicked(object sender, EventArgs e) 
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}

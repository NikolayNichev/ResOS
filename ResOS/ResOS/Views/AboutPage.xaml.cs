using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResOS.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public async void OpenMenu()         
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ItemsPage());
        }
    }
}
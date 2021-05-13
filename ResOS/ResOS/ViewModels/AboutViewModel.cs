using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ResOS.Views;
using System.Threading.Tasks;

namespace ResOS.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            OpenMenuCommand = new Command(async () => await OpenMenu());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenMenuCommand { get; }

        public async Task OpenMenu()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ItemsPage());
        }
    }
}
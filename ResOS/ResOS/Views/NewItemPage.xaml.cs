using ResOS.Models;
using ResOS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResOS.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Models.MenuItems Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
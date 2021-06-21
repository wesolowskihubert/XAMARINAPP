﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ogloszeniahubert.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Tblogout_Clicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.AccessToken = "";
            Navigation.InsertPageBefore(new SignInPage(), this);
            Navigation.PopAsync();
        }

        private void TapSearch_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindOgloszeniePage());
        }

        private void TapAddOgloszenie_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOgloszeniePage());
        }

        private void TapLastestOgloszenia_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LastestOgloszeniaPage());
        }

        private void TapHelp_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}
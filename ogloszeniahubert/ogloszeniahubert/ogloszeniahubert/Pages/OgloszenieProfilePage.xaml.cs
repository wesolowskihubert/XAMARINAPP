using ogloszeniahubert.Models;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ogloszeniahubert.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OgloszenieProfilePage : ContentPage
    {
        private string _email;
        private string _phoneNumber;
        private string _imie;
        private double _lat;
        private double _lon;
        public OgloszenieProfilePage(OgloszeniaUser ogloszeniaUser)
        {
            InitializeComponent();
            ImgOgloszenie.Source = ogloszeniaUser.FullLogoPath;
            LblProfileName.Text = ogloszeniaUser.Item;
            LblWojewodztwo.Text = ogloszeniaUser.Wojewodztwo;
            LblKategoria.Text = ogloszeniaUser.Category;
            _email = ogloszeniaUser.Email;
            _phoneNumber = ogloszeniaUser.Phone;
            _imie = ogloszeniaUser.UserName;
            _lat = ogloszeniaUser.Lat;
            _lon = ogloszeniaUser.Lon;
        }

        private void TapEmails_OnTapped(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {  
                emailMessenger.SendEmail(_email, "Zapytanie ofertowe - ogłoszenie", "Dzień dobry, uzytkowniku "+_imie+", kontaktuje sie w sprawie ogłoszenia w aplikacji Ogłoszonka.");
            }
        }

            private void TapPhone_OnTapped(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phoneNumber);
        }

        private async void TapMapa_OnTapped(object sender, EventArgs e)
        {

            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

            var lat = _lat;
            var lon = _lon;

            try
            {
                await Map.OpenAsync(new Location(latitude: lat, longitude: lon), options);
            }
            catch (Exception ex)
            {
                // No map application available to open
            }
        }
    }
}
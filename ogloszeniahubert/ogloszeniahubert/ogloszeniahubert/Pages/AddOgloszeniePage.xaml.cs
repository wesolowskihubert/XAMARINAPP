using ogloszeniahubert.Helper;
using ogloszeniahubert.Models;
using ogloszeniahubert.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddOgloszeniePage : ContentPage
    {
        public MediaFile file;
        private byte[] imageArray;
        public AddOgloszeniePage()
        {
            InitializeComponent();
        }

        private async void TapOpenCamera_OnTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

             file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgOgloszenie.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            imageArray = FilesHelper.ReadFully(file.GetStream());
        }

        private async void BntSubmit_OnClicked(object sender, EventArgs e)
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));

            //resultLocation.Text = $"lat: {result.Latitude}, lng: {result.Longitude}";

            var wojewodztwo = PickerWojewodztwo.Items[PickerWojewodztwo.SelectedIndex];
            var kategoria = PickerKategoria.Items[PickerKategoria.SelectedIndex];

            DateTime dateTime = DateTime.Now;
            int d = Convert.ToInt32(dateTime.ToOADate());
            
            var ogloszeniaUser = new OgloszeniaUser()
            {
                UserName = EntName.Text,
                Phone = EntPhone.Text,
                Email = EntEmail.Text,
                Item = EntItem.Text,
                Wojewodztwo = wojewodztwo,
                Category = kategoria,
                ImageArray = imageArray,
                Date = d,
                Lat = result.Latitude,
                Lon = result.Longitude
            };
            ApiServices apiServices = new ApiServices();
            var response = await apiServices.AddItem(ogloszeniaUser);
            if (!response)
            {
                await DisplayAlert("Błąd", "Nie mozna dodac ogłoszenia", "Zamknij");
            }
            else
            {
                await DisplayAlert("Sukces", "Ogłoszenie zostało dodane", "Zamknij");
                await Navigation.PopAsync();
            }
        }
    }
}
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
    public partial class NavToPage : ContentPage
    {
        public NavToPage()
        {
            InitializeComponent();
        }

        private async void BtnGetLocation_OnClicked(object sender, EventArgs e)
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));

            resultLocation.Text = $"lat: {result.Latitude}, lng: {result.Longitude}";
        }

        private async void BtnNavigateMaps_OnClicked(object sender, EventArgs e)
        {
            
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

            var lat = 52.4878;
            var lon = 16.9314;

            try
            {
                await Map.OpenAsync(new Location(latitude:lat,longitude:lon), options);
            }
            catch (Exception ex)
            {
                // No map application available to open
            }
        }
    }
}
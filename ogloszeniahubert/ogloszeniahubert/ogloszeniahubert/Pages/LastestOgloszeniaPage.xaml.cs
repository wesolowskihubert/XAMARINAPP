using ogloszeniahubert.Models;
using ogloszeniahubert.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ogloszeniahubert.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LastestOgloszeniaPage : ContentPage
    {
        public ObservableCollection<OgloszeniaUser> OgloszeniaUsers;

        public LastestOgloszeniaPage()
        {
            InitializeComponent();
            OgloszeniaUsers = new ObservableCollection<OgloszeniaUser>();
            FindItems();
        }

        private async void FindItems()
        {
            ApiServices apiServices = new ApiServices();
            var items = await apiServices.LastestItem();
            foreach (var wojewodztwo in items.ToList())
            {
                OgloszeniaUsers.Add(wojewodztwo);
            }
            LvItems.ItemsSource = OgloszeniaUsers;
        }

        private void LvItems_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedOgloszenie = e.SelectedItem as OgloszeniaUser;
            if (selectedOgloszenie != null)
            {
                Navigation.PushAsync(new OgloszenieProfilePage(selectedOgloszenie));
            }
            ((ListView)sender).SelectedItem = null;
            
        }
    }
}
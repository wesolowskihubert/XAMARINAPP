using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ogloszeniahubert.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindOgloszeniePage : ContentPage
    {
        public FindOgloszeniePage()
        {
            InitializeComponent();
        }

        private void BtnSearch_OnClicked(object sender, EventArgs e)
        {
            var kategoria = PickerKategoria.Items[PickerKategoria.SelectedIndex];
            var wojewodztwo = PickerWojewodztwo.Items[PickerWojewodztwo.SelectedIndex];

            Navigation.PushAsync(new ItemsListPage(kategoria,wojewodztwo));
        }
    }
}
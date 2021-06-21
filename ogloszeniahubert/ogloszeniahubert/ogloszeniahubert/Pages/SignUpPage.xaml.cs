using ogloszeniahubert.Services;
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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Błąd", "Nie udało się zarejestować", "Zamknij");
            } else
            {
                await DisplayAlert("Sukces", "Udało się zarejestować", "Zaczynamy");
                await Navigation.PopToRootAsync();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reikartz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page11 : ContentPage
    {
        public Page11()
        {
            InitializeComponent();
        }
        private async void B1(object sender, EventArgs e)
        {
            if (PasswordEntry.Text == "1937") 
            {
                var hotel = (Hotel)BindingContext;
                Page1 page = new Page1();
                page.BindingContext = hotel;
                await Navigation.PushAsync(page);
            }
            else await DisplayAlert("Error", "Password is wrong!", "Cancel");
        }
        private async void B2(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
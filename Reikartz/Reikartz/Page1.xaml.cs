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
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Image_hotel.Source = ImageSource.FromResource("Reikartz.Images.hotel.png");
        }
        private async void B1(object sender, EventArgs e)
        {
            Hotel.tomorrow?.Invoke();  //set tomorrow's date and check status of rooms
            string date = "Today is " + Convert.ToString(Hotel.current_date.Day) + "." +
                Convert.ToString(Hotel.current_date.Month) + "." + Convert.ToString(Hotel.current_date.Year);
            await DisplayAlert("",date,"OK");
        }
        private async void B2(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            string money = "In settlement account is  " + Convert.ToString(hotel.get_settlement_account()) + "  uah";
            await DisplayAlert("", money, "OK");
        }
        private async void B3(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page12 page = new Page12();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B4(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
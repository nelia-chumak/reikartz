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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            Image_hotel.Source = ImageSource.FromResource("Reikartz.Images.hotel.png");
        }
        private async void B1(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page21 page = new Page21();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B2(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page22 page = new Page22();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B3(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page25 page = new Page25();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B4(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page12 page = new Page12();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B5(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
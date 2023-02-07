using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hotel_library;

namespace Reikartz
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Image_hot.Source = ImageSource.FromResource("Reikartz.Images.hot.png");
        }
        private async void B1(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page11 page = new Page11();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void B2(object sender, EventArgs e)
        {
            var hotel = (Hotel)BindingContext;
            Page2 page = new Page2();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private void B3(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}

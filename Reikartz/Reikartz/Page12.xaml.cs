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
    public partial class Page12 : ContentPage
    {
        public Page12()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var hotel = (Hotel)BindingContext;
            Numbers.ItemsSource = hotel.rooms;
            base.OnAppearing();
        }
        private async void T1(object sender, ItemTappedEventArgs e)
        {
            Room SelectedNumber = e.Item as Room;
            if (SelectedNumber is JuniorSuiteRoom)
            {
                Page6 page = new Page6();
                page.BindingContext = SelectedNumber;
                await Navigation.PushAsync(page);
            }
            if (SelectedNumber is SuiteRoom)
            {
                Page7 page = new Page7();
                page.BindingContext = SelectedNumber;
                await Navigation.PushAsync(page);
            }
            if (SelectedNumber is StandardRoom)
            {
                Page5 page = new Page5();
                page.BindingContext = SelectedNumber;
                await Navigation.PushAsync(page);
            }
        }
    }
}
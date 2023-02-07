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
    public partial class Page6 : ContentPage
    {
        public Page6()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var r = (JuniorSuiteRoom)BindingContext;
            L1.Text = "Info about room #" + Convert.ToString(r.number) + " :";
            if (r.data_of_renting.rented == false) L2.Text = "It is available now";
            else L2.Text = "It isn't available now";
            L3.Text = "Area of room is " + Convert.ToString(r.area) + " square meters";
            L4.Text = "Number of places is " + Convert.ToString(r.number_of_places);
            L5.Text = "Availability of Wifi is " + Convert.ToString(r.Wifi);
            if (r.support_3D_on_TV == true) L6.Text = "In this room is 3-d TV";
            else L5.Text = "In this room isn't 3-d TV";
        }
    }
}
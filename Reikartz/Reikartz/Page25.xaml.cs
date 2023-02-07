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
    public partial class Page25 : ContentPage
    {
        public Page25()
        {
            InitializeComponent();
            SL.BackgroundColor = Color.FromHex("FFCCBC");
        }
        private async void B1(object sender, EventArgs e)
        {
            try
            {
                var hotel = (Hotel)BindingContext;
                var number = Convert.ToInt32(E1.Text);
                var amount = Convert.ToInt32(E2.Text);
                if (number <= hotel.rooms.Length)
                {
                    //if room is rented now
                    if (hotel.rooms[number - 1].data_of_renting.rented)
                    {
                        //temp variables
                        Room room = hotel.rooms[number - 1];
                        DateTime date_temp = room.data_of_renting.date_to;
                        room.data_of_renting += amount;
                        hotel.Put_on_settlement_account(hotel.get_price(number, room.data_of_renting.guests, date_temp, room.data_of_renting.date_to));
                        await DisplayAlert("","Operation was successfully completed","OK");
                    }
                    else await DisplayAlert("Error", "This number is not currently rented!", "Cancel");
                }
                else await DisplayAlert("Error", "This number is not exist!", "Cancel");
            }
            catch (System.FormatException)
            {
                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
            }
            catch (System.OverflowException)
            {
                PrintBuildInException("You entered data is too big. Fill it all over again");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
            }
            catch (RoomNotFoundException exception)
            {
                PrintException(exception);
            }
            catch (RoomIsRented exception)
            {
                PrintException(exception);
            }
        }
        private async void B2(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        //methods for print info in red
        private async void PrintException(Exception exception)
        {
            await DisplayAlert("Error", exception.Message, "Cancel");
        }
        private async void PrintBuildInException(string mess)
        {
            await DisplayAlert("Error", mess, "Cancel");
        }
    }
}
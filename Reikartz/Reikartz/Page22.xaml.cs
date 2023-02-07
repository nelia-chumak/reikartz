using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace Reikartz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page22 : ContentPage
    {
        public Page22()
        {
            InitializeComponent();
            SL.BackgroundColor = Color.FromHex("FFCCBC");
            date.Format = "Short";
            date.Date = DateTime.Today;
            date.MinimumDate = DateTime.Today.AddDays(1);
            date.MaximumDate = DateTime.Today.AddDays(60);
            E4.Text = Convert.ToString(DateTime.Today.AddDays(1).Day) + "." + Convert.ToString(DateTime.Today.AddDays(1).Month) + "." + Convert.ToString(DateTime.Today.AddDays(1).Year);
        }
        private async void B1(object sender, EventArgs e)
        {
            try
            {
                var hotel = (Hotel)BindingContext;
                int number = Convert.ToInt32(E1.Text)-1, colvo = Convert.ToInt32(E2.Text),
                    year_to = 0, month_to = 0, day_to = 0;
                (year_to, month_to, day_to) = SplitRead(E4.Text);
                DateTime from = Hotel.current_date;
                DateTime to = new DateTime(year_to, month_to, day_to);
                Guest[] guests = new Guest[colvo];
                for(int i=0; i < colvo; i++)
                {
                    guests[i] = new Guest(); 
                }
                var price = hotel.get_price(number, guests, from, to);
                bool flag = await DisplayAlert("", "Price is " + Convert.ToString(price) + "\nDo you want to rent this number?", "Yes", "No");
                if (flag)
                {
                    for (int i = 0; i < colvo; i++)
                    {
                        Page23 page = new Page23(1, number - 1, i);
                        page.BindingContext = hotel;
                        await PopupNavigation.Instance.PushAsync(page);
                    }
                    hotel.Rent(BookHandler, number, guests, year_to, month_to, day_to);
                }
                Navigation.PopAsync();
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
            catch (RoomIsBooked exception)
            {
                PrintException(exception);
            }
            catch (ImpossibleAge exception)
            {
                PrintException(exception);
            }
            catch (TooManyGuests exception)
            {
                PrintException(exception);
            }
            catch (TooFewGuests exception)
            {
                PrintException(exception);
            }
            catch (EndDateIsLessThenStartDate exception)
            {
                PrintException(exception);
            }
        }
        private async void B2(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private static (int year, int day, int month) SplitRead(string date)
        {
            //variables for position tracking
            int count = 0;
            bool f = true;
            //variables for day, month and year value
            string year = "", day = "", month = "";
            for (int i = 0; i < date.Length; i++)
            {
                //to the first point, it's day
                if (date[i] != '.' && count == 0)
                {
                    day += date[i];
                }
                else if (date[i] == '.' && count == 0)
                {
                    count++;
                    f = false;
                }
                //to the second - month
                if (date[i] != '.' && count == 1)
                {
                    month += date[i];
                    f = true;
                }
                else if (date[i] == '.' && count == 1 && f == true) count++;
                //then year
                if (date[i] != '.' && count == 2)
                {
                    year += date[i];
                }
            }
            return (Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
        }
        private void D1(object sender, DateChangedEventArgs e)
        {
            DateTime date_t = date.Date;
            E4.Text = Convert.ToString(date_t.Day) + "." + Convert.ToString(date_t.Month) + "." + Convert.ToString(date_t.Year);
        }
        //print messages for events
        private async void BookHandler(object sender, RoomEventArgs e)
        {
            await DisplayAlert("", e.message, "Ok");
        }
        private async void PrintException(Exception exception)
        {
            var hotel = (Hotel)BindingContext;
            await DisplayAlert("Error", exception.Message, "Cancel");
            await Navigation.PopAsync();
            Page22 page = new Page22();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void PrintBuildInException(string mess)
        {
            var hotel = (Hotel)BindingContext;
            await DisplayAlert("Error", mess, "Cancel");
            await Navigation.PopAsync();
            Page22 page = new Page22();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
    }
}
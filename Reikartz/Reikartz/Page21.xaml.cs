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
    public partial class Page21 : ContentPage
    {
        public Page21()
        {
            InitializeComponent();
            SL.BackgroundColor = Color.FromHex("FFCCBC");
            date1.Format = "Short";
            date1.MinimumDate = DateTime.Today.AddDays(1);
            date1.MaximumDate = DateTime.Today.AddDays(60);
            E3.Text = Convert.ToString(DateTime.Today.AddDays(1).Day) + "." + Convert.ToString(DateTime.Today.AddDays(1).Month) + "." + Convert.ToString(DateTime.Today.AddDays(1).Year);
            date2.Format = "Short";
            date2.MinimumDate = DateTime.Today.AddDays(2);
            date2.MaximumDate = DateTime.Today.AddDays(60);
            E4.Text = Convert.ToString(DateTime.Today.AddDays(2).Day) + "." + Convert.ToString(DateTime.Today.AddDays(2).Month) + "." + Convert.ToString(DateTime.Today.AddDays(2).Year);
        }
        private async void B1(object sender, EventArgs e)
        {
            try
            {
                var hotel = (Hotel)BindingContext;
                int number = Convert.ToInt32(E1.Text), colvo = Convert.ToInt32(E2.Text),
                    year_from = 0, month_from = 0, day_from = 0, year_to = 0, month_to = 0, day_to = 0;
                (year_from, month_from, day_from) = SplitRead(E3.Text);
                (year_to, month_to, day_to) = SplitRead(E4.Text);
                DateTime from = new DateTime(year_from, month_from, day_from);
                DateTime to = new DateTime(year_to, month_to, day_to);
                Guest[] guests = new Guest[colvo];
                var price = hotel.get_price(number, guests, from, to);
                bool flag = await DisplayAlert("", "Price is " + Convert.ToString(price) + "\nDo you want to book this number?", "Yes", "No");
                if (flag)
                {
                    for (int i = 0; i < colvo; i++)
                    {
                        Page23 page = new Page23(1, number - 1, i);
                        page.BindingContext = hotel;
                        await PopupNavigation.Instance.PushAsync(page);
                    }
                    hotel.Book(BookHandler, number, guests, year_from, month_from, day_from, year_to, month_to, day_to);
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
        private void D1(object sender, DateChangedEventArgs e)
        {
            DateTime date_t = date1.Date;
            E3.Text = Convert.ToString(date_t.Day) + "." + Convert.ToString(date_t.Month) + "." + Convert.ToString(date_t.Year);
        }
        private void D2(object sender, DateChangedEventArgs e)
        {
            DateTime date_t = date2.Date;
            E4.Text = Convert.ToString(date_t.Day) + "." + Convert.ToString(date_t.Month) + "." + Convert.ToString(date_t.Year);
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
        //print messages for events
        private async void BookHandler(object sender, RoomEventArgs e)
        {
            await DisplayAlert("",e.message,"Ok");
        }
        private async void PrintException(Exception exception)
        {
            var hotel = (Hotel)BindingContext;
            await DisplayAlert("Error", exception.Message, "Cancel");
            await Navigation.PopAsync();
            Page21 page = new Page21();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
        private async void PrintBuildInException(string mess)
        {
            var hotel = (Hotel)BindingContext;
            await DisplayAlert("Error", mess, "Cancel");
            await Navigation.PopAsync();
            Page21 page = new Page21();
            page.BindingContext = hotel;
            await Navigation.PushAsync(page);
        }
    }
}
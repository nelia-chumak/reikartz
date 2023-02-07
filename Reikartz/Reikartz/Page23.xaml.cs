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
    public partial class Page23
    {
        int type;
        int number;
        int i;
        public event EventHandler<EventArgs> OperationCompeleted;
        public Page23(int _type, int _number, int _i)
        {
            type = _type;
            number = _number;
            i = _i;
            InitializeComponent();
            SL.BackgroundColor = Color.FromHex("FFCDD2");
            date.Format = "Short";
            date.Date = DateTime.Today;
            date.MinimumDate = DateTime.Today.AddYears(-100);
            date.MaximumDate = DateTime.Today;
            E2.Text = Convert.ToString(DateTime.Today.Day)+"."+ Convert.ToString(DateTime.Today.Month)+"."+ Convert.ToString(DateTime.Today.Year);
        }
        private void B1(object sender, EventArgs e)
        {
            try
            {
                var hotel = (Hotel)BindingContext;
                int year = 0, month = 0, day = 0;
                (year, month, day) = SplitRead(E2.Text);
                Guest g = new Guest(E1.Text, year, month, day, Convert.ToInt32(E3.Text), picker.Items[picker.SelectedIndex]);
                if (type == 1) hotel.rooms[number].data_of_booking[hotel.rooms[number].data_of_booking.Length - 2].guests[i] = g;
                if (type == 2) hotel.rooms[number].data_of_renting.guests[i] = g;
                this.BindingContext = hotel;
                OperationCompeleted?.Invoke(this, EventArgs.Empty);
                PopupNavigation.Instance.PopAsync();
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
            E2.Text = Convert.ToString(date_t.Day) + "." + Convert.ToString(date_t.Month) + "." + Convert.ToString(date_t.Year);
        }
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
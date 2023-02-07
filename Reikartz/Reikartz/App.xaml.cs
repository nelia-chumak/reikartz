using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hotel_library;

namespace Reikartz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Hotel _Reikartz = new Hotel("Reikartz");                             //create new hotel
            _Reikartz.rooms = new Room[3];                                       //craete roomms array
            _Reikartz.rooms[0] = new StandardRoom(1, 20, 1, true, "st.png");               //create first room
            _Reikartz.rooms[1] = new JuniorSuiteRoom(2, 30, 2, true, false, "js.png");     //second
            _Reikartz.rooms[2] = new SuiteRoom(3, 40, 3, true, 2, "ordinary", "s.png");    //third
            MainPage Main = new MainPage();
            NavigationPage.SetHasNavigationBar(Main, false);
            Main.BindingContext = _Reikartz;
            MainPage = new NavigationPage(Main);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Hotel_library
{
    public class Guest : INotifyPropertyChanged
    {
        private string name;            //guest name
        private DateTime date_of_birth; //guest date of birth
        private int passport_ID;        //guest passport ID
        private string gender;          //guest gender

        //constructor with parameters
        public Guest(string name1="a", int year=2000, int month=1, int day=1, int passport_ID1=123456789, string gender1="feminin")
        {
            DateTime date_of_birth_temp = new DateTime(year, month, day);  //create temporary variable for date of birth
            //check this value: if guest is older then 100 years
            if ((DateTime.Today - date_of_birth_temp).TotalDays / 365 > 100)
                throw new ImpossibleAge("This age is greater than possible!"); //trow exception type ImpossibleAge
            else if ((DateTime.Today - date_of_birth_temp).TotalDays / 365 < 18)
                throw new ImpossibleAge("This age is smaller than possible!"); //trow exception type ImpossibleAge
            //check name for availability of letters, gender and passport ID for right input
            else if (!ContainsLetters(name1) || (gender1 != "feminin" && gender1 != "masculin") || passport_ID1 < 100000000)
                throw new System.FormatException(); //trow exception type FormatException
            else
            {
                name = name1;
                date_of_birth = date_of_birth_temp;
                passport_ID = passport_ID1;
                gender = gender1;
            }
        }
        //method for checking availability of letters in string
        private bool ContainsLetters(string str)
        {
            for (int i = 0; i < str.Length; i++)         //walking througt all characters
                if (char.IsLetter(str[i])) return true;  //if character is a letter then return true
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

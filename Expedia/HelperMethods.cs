using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia
{
    internal class HelperMethods
    {



        public const string MAIL_MESSAGE = "Enter your Email : ";
        public const string FIRST_NAME_MESSAGE = "Enter the first name : ";
        public const string SECOND_NAME_MESSAGE = "Enter the Second name : ";
        public const string TRY_AGAIN_MESSAGE = "To try again press 1 , Otherwise press any key.";
        public const string PHONE_NUMBER_MESSAGE = "Enter your phone number: +20 ";
        public const string EMERGENCY_NUMBER_MESSAGE = "Enter your emergency number: +20 ";
        public const string DATE_TIME_MESSAGE = "Enter your birthdate (e.g., MM/dd/yyyy): ";
        public const string GENDER_MESSAGE = "Please enter your gender ('M' for male, 'F' for female): ";
        public const string ACCESSIBILITY_NEEDS_MESSAGE = "Do you have any Accessibility needs (YES/NO) ? ";


        static public User MakeUser (string data)
        {
            string[] parts = data.Split('|');
            // Create a new User object and assign values from the parts
            User user = new User
            (
                parts[1], // Email
                parts[2], // First Name
                parts[3], // Last Name
                parts[4], // Phone Number
                parts[5], // Emergency Number
                DateTime.Parse(parts[6]), // BirthDate
                bool.Parse(parts[7]), // IsMale
                int.Parse(parts[0]) // SystemId
            );

            return user;
        }

        static public bool ValidatePassword()
        {
            return true;
        }

        static public bool ValidateName(string name)
        {
            foreach (char c in name)
            {
                if(c >= '0' && c <= '9') 
                    return false;
            }
            return true;
        }

        static public bool ValidateEmail(string Email)
        {
            
            for (int i = 0; i < Email.Length; i++)
            {
                if (Email[i] == '@') 
                    return true;
            }
            return false;
        }

        static public bool ValidateDateAndTime(string DateAndTime)
        {
            DateTime dateTime = new DateTime();
            return DateTime.TryParse(DateAndTime, out dateTime) && dateTime < DateTime.Now;

        }

        static public bool ValidatePhoneNumber(string PhoneNumber)
        {
            string starting = PhoneNumber.Substring(0, 2);
            if (PhoneNumber.Length == 10 && (starting == "10" || starting == "11" || starting == "12" || starting == "15")) return true;
            else return false;
        }

        public static bool ValidateGender(string gender)
        {
            return gender.ToUpper() == "M" || gender.ToUpper() == "F";
        }

        static public String GetStringInput(string MessageToDisplay)
        {
            string _input;
            Console.Write(MessageToDisplay);
            _input= Console.ReadLine();
            Console.WriteLine();
            return _input;
        }

        static public string DateTimeYearOnly (DateTime date)
        {
            string ret = "";
            ret += date.Month;
            ret += '/';
            ret += date.Day;
            ret += '/';
            ret += date.Year;
            return ret;
        }
    }
}

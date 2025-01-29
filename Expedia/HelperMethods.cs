using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Expedia
{
    internal class HelperMethods
    {


        public static class Message
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
            public const string PASSWORD_MESSAGE = "Please enter your password.\n" +
                "- Between 8 and 30 letter\n" +
                "- Should contain at least one uppercase letter , lowercase letter , digit , and special character  (!,@,$,#,%,$,&,*)\n" +
                "Password: ";
            public const string CHANGEDATAMESSGE = "" +
                "1 - Change Password\n" +
                "2 - Phone Number\n" +
                "3 - Emergency Number\n";
        }

        public static User ConvertDataToUser (string data)
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
                parts[7] == "True"? true : false,
                int.Parse(parts[0]), // SystemId
                parts[8] // password
            );

            Console.WriteLine(parts.Length);
            for (int i = 9; i < parts.Length; ++i) user.AddFlight(int.Parse(parts[i]));
            return user;
        }

        static public bool ValidatePassword()
        {
            return true;
        }

        public static bool ValidateName(string name)
        {
            foreach (char c in name)
            {
                if(c >= '0' && c <= '9') 
                    return false;
            }
            return true;
        }

        public static bool ValidateEmail(string Email)
        {
            
            for (int i = 0; i < Email.Length; i++)
            {
                if (Email[i] == '@') 
                    return true;
            }
            return false;
        }

        public static bool ValidateDateAndTime(string DateAndTime)
        {
            DateTime dateTime = new DateTime();
            return DateTime.TryParse(DateAndTime, out dateTime) && dateTime < DateTime.Now;

        }

        public static bool ValidatePhoneNumber(string PhoneNumber)
        {
            string starting = PhoneNumber.Substring(0, 2);
            if (PhoneNumber.Length == 10 && (starting == "10" || starting == "11" || starting == "12" || starting == "15")) return true;
            else return false;
        }

        public static bool ValidatePassword(string password) 
        { 
            bool upperCase = false , lowerCase = false, digit = false, special = false;
            
            if(password.Length > 30 || password.Length < 8) return false;

            foreach (char c in password)
            {
                if(c >= 'A' && c <= 'Z') upperCase = true;
                if(c >= 'a' && c <= 'z') lowerCase = true;
                if(c >= '0' && c <= '9') digit = true;
                if (c == '_' || c == '@' || c == '%' || c == '$' || c == '!' || c == '#' || c == '&') special = true;               
            }
            return upperCase && lowerCase && digit && special;
        
        }

        public static bool ValidateGender(string gender)
        {
            return gender.ToUpper() == "M" || gender.ToUpper() == "F";
        }

        public static string encryptedInput()
        {
            string input = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break; 
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return input;
        }

        public static bool confirmationPassword(string storedHashedPassword)
        {
            Console.WriteLine("Enter the password again : ");
            string confirmationPassword = HelperMethods.encryptedInput();

            string hashedConfirmationPassword = HashPassword(confirmationPassword);

            bool isValidConfirmationPassword = (hashedConfirmationPassword == storedHashedPassword);
            
            while (!isValidConfirmationPassword)
            {
                Console.WriteLine("The entered password is not identical to the first password. " + Message.TRY_AGAIN_MESSAGE);
                short _option = short.Parse(Console.ReadLine());
                if (_option == 1)
                {
                    Console.WriteLine("Enter the password again : ");
                    confirmationPassword = HelperMethods.encryptedInput();
                    hashedConfirmationPassword = HashPassword(confirmationPassword);
                    isValidConfirmationPassword = (hashedConfirmationPassword == storedHashedPassword);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static String GetStringInput(string MessageToDisplay)
        {
            string _input;
            Console.Write(MessageToDisplay);
            _input= Console.ReadLine();
            return _input;
        }

        public static string DateTimeYearOnly (DateTime date)
        {
            string ret = "";
            ret += date.Month;
            ret += '/';
            ret += date.Day;
            ret += '/';
            ret += date.Year;
            return ret;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hash;
            }
        }


        public static string GetValidatedInput(string message, Func<string, bool> validate, string errorMessage)
        {
            // The method prompts the user for input, validates it using a provided validation function,
            // and repeats the process until valid input is entered. 

            string input = HelperMethods.GetStringInput(message);
            while (!validate(input))
            {
                Console.WriteLine(errorMessage + "\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                char option = char.Parse(Console.ReadLine());
                if (option != '1') return "!";
                input = HelperMethods.GetStringInput(message);
            }
            return input;
        }

        public static string GetPassword()
        {

            // Taking the password of the user , validating it , and encrypting and hashing it.

            Console.WriteLine(HelperMethods.Message.PASSWORD_MESSAGE);
            string password = HelperMethods.encryptedInput();


            bool isValidPassword = HelperMethods.ValidatePassword(password);
            if (!isValidPassword)
            {
                Console.WriteLine("The password is invalid.\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                char _Option = char.Parse(Console.ReadLine());
                if (_Option == '1')
                {
                    return GetPassword();
                }
                else
                {
                    return "!";
                }
            }

            string hashedPassword = HelperMethods.HashPassword(password);


            if (!HelperMethods.confirmationPassword(hashedPassword))
            {
                return "!";
            }

            return hashedPassword;
        }

    }
}

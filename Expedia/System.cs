using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
namespace Expedia
{
    internal class System
    {

        const string USERS_FILE_PATH = @"D:\Projects\Projects\C#\Expedia\DataBase\Users.txt";
       
        public List <User> _SystemUsers = new List <User> ();

        void LoadUserDataBase()
        {
            _SystemUsers.Clear();
            using (StreamReader reader = new StreamReader(USERS_FILE_PATH))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _SystemUsers.Add(HelperMethods.MakeUser(line));
                }
            }
        }

        void UpdateUserDataBase()
        {
            using (StreamWriter writer = new StreamWriter(USERS_FILE_PATH , true))
            {
                foreach (var user in _SystemUsers)
                {
                    writer.WriteLine(user.data());
                }
            }
        }

        private string GetEmail()
        {
            string email = HelperMethods.GetStringInput(HelperMethods.MAIL_MESSAGE);
            bool IsValidEmail = HelperMethods.ValidateEmail(email) && (_SystemUsers.Exists(user => user.Email == email) == false);

            // Validating the email taken by the user , using some helper methods
            while (!IsValidEmail)
            {
                if (_SystemUsers.Exists(user => user.Email == email))
                {
                    Console.WriteLine("This email already exsits\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                }
                else
                {
                    Console.WriteLine("The email is invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                }

                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    email = HelperMethods.GetStringInput(HelperMethods.MAIL_MESSAGE);
                    IsValidEmail = (HelperMethods.ValidateEmail(email) && (_SystemUsers.Exists(user => user.Email == email) == false));
                }
                else
                {
                    return "!";
                }
            }
            return email;
        }

        private string GetFirstName()
        {

            string firstName = HelperMethods.GetStringInput(HelperMethods.FIRST_NAME_MESSAGE);
            bool isValidName = HelperMethods.ValidateName(firstName);
            while (!isValidName)
            {
                Console.WriteLine("The first name is invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    firstName = HelperMethods.GetStringInput(HelperMethods.FIRST_NAME_MESSAGE);
                    isValidName = HelperMethods.ValidateName(firstName);
                }
                else
                {
                    return "!";
                }
            }
            return firstName;
        }

        private string GetLastName()
        {
            string secondName = HelperMethods.GetStringInput(HelperMethods.SECOND_NAME_MESSAGE);
            bool isValidName = HelperMethods.ValidateName(secondName);
            while (!isValidName)
            {
                Console.WriteLine("The second name is invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    secondName = HelperMethods.GetStringInput(HelperMethods.SECOND_NAME_MESSAGE);
                    isValidName = HelperMethods.ValidateName(secondName);
                }
                else
                {
                    return "!";
                }
            }
            return secondName;
        }

        private string GetPhoneNumber()
        {
            string phoneNumber = HelperMethods.GetStringInput(HelperMethods.PHONE_NUMBER_MESSAGE);
            bool isValidPhoneNumber = HelperMethods.ValidatePhoneNumber(phoneNumber);
            while (!isValidPhoneNumber)
            {
                Console.WriteLine("The phone number is invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    phoneNumber = HelperMethods.GetStringInput(HelperMethods.PHONE_NUMBER_MESSAGE);
                    isValidPhoneNumber = HelperMethods.ValidatePhoneNumber(phoneNumber);
                }
                else
                {
                    return "!";
                }
            }
            return phoneNumber;
        }

        private string GetEmergancyNumber()
        {
            string phoneNumber = HelperMethods.GetStringInput(HelperMethods.EMERGENCY_NUMBER_MESSAGE);
            bool isValidPhoneNumber = HelperMethods.ValidatePhoneNumber(phoneNumber);
            while (!isValidPhoneNumber)
            {
                Console.WriteLine("The emergacny number is invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    phoneNumber = HelperMethods.GetStringInput(HelperMethods.EMERGENCY_NUMBER_MESSAGE);
                    isValidPhoneNumber = HelperMethods.ValidatePhoneNumber(phoneNumber);
                }
                else
                {
                    return "!";
                }
            }
            return phoneNumber;
        }

        private string GetDateTime()
        {
            string dateTimeInput = HelperMethods.GetStringInput(HelperMethods.DATE_TIME_MESSAGE);
            bool isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
            while (!isValidDateTime)
            {
                Console.WriteLine("The date are invalid.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    // User chooses to try again
                    dateTimeInput = HelperMethods.GetStringInput(HelperMethods.DATE_TIME_MESSAGE);
                    isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
                }
                else
                {
                    return "!";
                }
            }

            return dateTimeInput;
        }

        private bool checkIfContinue(string value)
        {
            return value != "!";
        }

        private bool checkIfContinue(int x)
        {
            return x != 3;
        }

        private int GetGender()
        {
            string genderInput = HelperMethods.GetStringInput(HelperMethods.GENDER_MESSAGE);
            bool isValidGender = HelperMethods.ValidateGender(genderInput);

            // Loop to validate the gender input until it's valid
            while (!isValidGender)
            {
                Console.WriteLine("Invalid gender input. Please enter 'M' or 'F'.\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                short _Option;
                _Option = short.Parse(Console.ReadLine());
                if (_Option == 1)
                {
                    genderInput = HelperMethods.GetStringInput(HelperMethods.GENDER_MESSAGE);
                    isValidGender = HelperMethods.ValidateGender(genderInput);
                }
                else
                {
                    return 3;
                }
            }

            // Return true if male, false if female
            if (genderInput.ToUpper() == "M") return 1;
            else return 0;
        }

        static bool LogIn()
        {
            return true;
        }
       
        private void SignUP()
        {

            string email = GetEmail();
            if (!checkIfContinue(email)) return;

            string firstName = GetFirstName();
            if(!checkIfContinue(firstName)) return;

            string lastName = GetLastName();
            if (!checkIfContinue(lastName)) return;

            string phoneNumber = GetPhoneNumber();
            if(!checkIfContinue(phoneNumber)) return;

            string emergencyNumber = GetEmergancyNumber();
            if (!checkIfContinue(emergencyNumber)) return;

            string dateTimeStringVersion = GetDateTime();
            if(!checkIfContinue(dateTimeStringVersion)) return;
            DateTime birthDate = new DateTime();
            DateTime.TryParse(dateTimeStringVersion, out birthDate);
            Console.WriteLine(birthDate);
            Console.WriteLine(HelperMethods.DateTimeYearOnly(birthDate));

            
            int gender = GetGender();
            if (!checkIfContinue(gender)) return;
            bool isMale = (gender == 1);


            User newUser = new User(email , firstName , lastName , phoneNumber, emergencyNumber , birthDate , isMale , _SystemUsers.Count);
            _SystemUsers.Add(newUser);

            UpdateUserDataBase();

            return;
        }

        private void MainMenu()
        {
           
            //The following code is essentialy for validataing the chocie made by the user
            
            bool run = true;
            while (run)
            {
                LoadUserDataBase();
                Console.WriteLine("Welcome to expedia.\n" +
                             "Choose the one of the following options:\n" +
                             "1) Log in. \n" +
                             "2) Sign up. \n" +
                             "3) Exit \n");


                short _Option = -1;
                while (_Option == -1)
                {
                    _Option = short.Parse(Console.ReadLine());
                    if (_Option == 3) break;
                    if (_Option != 1 && _Option != 2)
                    {
                        Console.WriteLine("Invalid Option\n" + HelperMethods.TRY_AGAIN_MESSAGE);
                        short _NewOption;
                        _NewOption = short.Parse(Console.ReadLine());
                        if (_NewOption != 1) return;
                        else _Option = -1;
                    }
                    if (_Option == -1)
                        Console.Write("Try Again : ");
                }


                if (_Option == 1)
                {
                    LogIn();
                }
                else if (_Option == 2)
                {
                    SignUP();
                }
                else
                {
                    run = false;
                }
            }
        }

        public void RunSystem()
        {
           MainMenu();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
namespace Expedia
{
    internal class System
    {

        const string USERS_FILE_PATH = @"D:\Projects\Projects\C#\Expedia\DataBase\Users.txt";
       
        public List <User> _SystemUsers = new List <User> ();



        private bool UserExsit(string email , string password)
        {
            // iterating over the users and validating if any of them matches with the given email and password
            foreach (User user in _SystemUsers)
            {
                if(user.isVaild(email, password))
                {
                    return true;
                }
            }
            return false;
        }

        void LoadUserDataBase()
        {
            // This method loads the data in the file to the _SystemUsers list

            if(_SystemUsers.Count != 0)
                _SystemUsers.Clear();
            using (StreamReader reader = new StreamReader(USERS_FILE_PATH))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _SystemUsers.Add(HelperMethods.DataToUser(line));
                }
            }
        }

        void UpdateUserDataBase()
        {
            // This method add the data in the list to the file , after deleting all the data in the other file 

            using (StreamWriter writer = new StreamWriter(USERS_FILE_PATH , false))
            {
                foreach (var user in _SystemUsers)
                {
                    writer.WriteLine(user.data());
                }
            }
        }

        private string GetValidatedInput(string message, Func<string, bool> validate, string errorMessage)
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

        private string GetDateTime()
        {
            // This method is responsable for getting an input in the form of a data [MM/DD/YYYY]

            string dateTimeInput = HelperMethods.GetStringInput(HelperMethods.Message.DATE_TIME_MESSAGE);
            bool isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
            while (!isValidDateTime)
            {
                Console.WriteLine("The date are invalid.\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                char _Option;
                _Option = char.Parse(Console.ReadLine());
                if (_Option == '1')
                {
                    // User chooses to try again
                    dateTimeInput = HelperMethods.GetStringInput(HelperMethods.Message.DATE_TIME_MESSAGE);
                    isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
                }
                else
                {
                    return "!";
                }
            }

            return dateTimeInput;
        }      

        private int GetGender()
        {
            // A method to find the gender of the user , male => 1 | female => 2 | user exit => 3

            string genderInput = HelperMethods.GetStringInput(HelperMethods.Message.GENDER_MESSAGE);
            bool isValidGender = HelperMethods.ValidateGender(genderInput);

            // Loop to validate the gender input until it's valid
            while (!isValidGender)
            {
                Console.WriteLine("Invalid gender input. Please enter 'M' or 'F'.\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                char _Option;
                _Option = char.Parse(Console.ReadLine());
                if (_Option == '1')
                {
                    genderInput = HelperMethods.GetStringInput(HelperMethods.Message.GENDER_MESSAGE);
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

        private string GetPassword()
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

        private bool LogIn()
        {
            string email = HelperMethods.GetStringInput(HelperMethods.Message.MAIL_MESSAGE);
            Console.Write("Please enter your password: ");
            string password = HelperMethods.encryptedInput();
            password = HelperMethods.HashPassword(password);
            bool validUserDate = UserExsit(email, password);
            password = HelperMethods.HashPassword(password);

            while (!validUserDate)
            {
                Console.WriteLine("Wrong Email or Password " + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                short _option = short.Parse(Console.ReadLine());
                if(_option == 1)
                {
                    email = HelperMethods.GetStringInput(HelperMethods.Message.MAIL_MESSAGE);
                    Console.Write("Please enter your password: ");
                    password = HelperMethods.encryptedInput();
                    password = HelperMethods.HashPassword(password);
                    validUserDate = UserExsit(email, password);
                }
                else
                {
                    return false;
                }
            }
            return true;

        }

        private void SignUP()
        {

           string userEmail = GetValidatedInput(
                                                HelperMethods.Message.MAIL_MESSAGE,
                                                email => HelperMethods.ValidateEmail(email) && !_SystemUsers.Exists(u => u.Email == email),
                                                "Invalid or duplicate email."
                                                );
            if (userEmail == "!") return;


            string firstName = GetValidatedInput(HelperMethods.Message.FIRST_NAME_MESSAGE , 
                                                 firstname => HelperMethods.ValidateName(firstname) ,
                                                 "The first name is invalid.\n"
                                                 );
            if(firstName == "!") return;

            string lastName = GetValidatedInput(HelperMethods.Message.SECOND_NAME_MESSAGE,
                                                lastname => HelperMethods.ValidateName(lastname),
                                                "The last name is invalid.\n"
                                                );
            if (lastName == "!") return;

            string phoneNumber = GetValidatedInput(HelperMethods.Message.PHONE_NUMBER_MESSAGE,
                                                   number => HelperMethods.ValidatePhoneNumber(number),
                                                   "The phone number is invalid.\n"
                                                );
            if (phoneNumber == "!") return;

            string emergencyNumber = GetValidatedInput(HelperMethods.Message.EMERGENCY_NUMBER_MESSAGE,
                                                       number => HelperMethods.ValidatePhoneNumber(number),
                                                       "The emergency number is invalid.\n"
                                                    );
            if (emergencyNumber == "!") return;

            string dateTimeStringVersion = GetDateTime();
            if(dateTimeStringVersion == "!") return;
            DateTime birthDate = new DateTime();
            DateTime.TryParse(dateTimeStringVersion, out birthDate);
          

            
            int gender = GetGender();
            if (gender == 3) return;
            bool isMale = (gender == 1); 

            
            string password = GetPassword();
          
            if (password == "!") return;
            
            User newUser = new User(userEmail, firstName , lastName , phoneNumber, emergencyNumber , birthDate , isMale , _SystemUsers.Count , password);
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
                        Console.WriteLine("Invalid Option\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
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
                    if (LogIn())
                        Console.WriteLine("I'M IIIIINNNNNNNNNNNNNNNNNNNNNN");
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

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
namespace Expedia
{
    internal class System
    {

        const string USERS_FILE_PATH = @"D:\Projects\Projects\C#\Expedia\Expedia\DataBase\Users.txt";
       
        static private List <User> ExpediaUsers = new List <User> ();
        User CurrentUser;


        private bool UserExsit(string email , string password)
        {
            // iterating over the users and validating if any of them matches with the given email and password
            foreach (User user in ExpediaUsers)
            {
                if(user.isVaild(email, password))
                {
                    return true;
                }
            }
            return false;
        }

        static void LoadUserDataBase()
        {
            // This method loads the data in the file to the _SystemUsers list

            if(ExpediaUsers.Count != 0)
                ExpediaUsers.Clear();
            using (StreamReader reader = new StreamReader(USERS_FILE_PATH))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ExpediaUsers.Add(HelperMethods.DataToUser(line));
                }
            }
        }

        static void UpdateUserDataBase()
        {
            // This method add the data in the list to the file , after deleting all the data in the other file 

            using (StreamWriter writer = new StreamWriter(USERS_FILE_PATH , false))
            {
                foreach (var user in ExpediaUsers)
                {
                    writer.WriteLine(user.data());
                }
            }
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

        private int GetId(string UserEmail)
        {
            int CurUser = 0;
            foreach (User user in ExpediaUsers)
            {
                if (user.Email == UserEmail)
                {
                    return CurUser;
                }
                CurUser++;
            }
            return CurUser;
        }

        private int LogIn()
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
                    return -1;
                }
            }

            // Return the idoftheuser

            return GetId(email);

        }

        private void SignUP()
        {

           string userEmail = HelperMethods.GetValidatedInput(
                                                HelperMethods.Message.MAIL_MESSAGE,
                                                email => HelperMethods.ValidateEmail(email) && !ExpediaUsers.Exists(u => u.Email == email),
                                                "Invalid or duplicate email."
                                                );
            if (userEmail == "!") return;


            string firstName = HelperMethods.GetValidatedInput(HelperMethods.Message.FIRST_NAME_MESSAGE , 
                                                 firstname => HelperMethods.ValidateName(firstname) ,
                                                 "The first name is invalid.\n"
                                                 );
            if(firstName == "!") return;

            string lastName = HelperMethods.GetValidatedInput(HelperMethods.Message.SECOND_NAME_MESSAGE,
                                                lastname => HelperMethods.ValidateName(lastname),
                                                "The last name is invalid.\n"
                                                );
            if (lastName == "!") return;

            string phoneNumber = HelperMethods.GetValidatedInput(HelperMethods.Message.PHONE_NUMBER_MESSAGE,
                                                   number => HelperMethods.ValidatePhoneNumber(number),
                                                   "The phone number is invalid.\n"
                                                );
            if (phoneNumber == "!") return;

            string emergencyNumber = HelperMethods.GetValidatedInput(HelperMethods.Message.EMERGENCY_NUMBER_MESSAGE,
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

            
            string password = HelperMethods.GetPassword();
          
            if (password == "!") return;
            
            User newUser = new User(userEmail, firstName , lastName , phoneNumber, emergencyNumber , birthDate , isMale , ExpediaUsers.Count , password);
            ExpediaUsers.Add(newUser);

         
            UpdateUserDataBase(); 

            return;
        }

        private void EnterSystem(User user)
        {
            CurrentUser = user;
            Session session = new Session(CurrentUser);
        }

        static public void AddUser(User user)
        {
            var UserId = user.ID;
            ExpediaUsers.Insert(UserId , user);
            UpdateUserDataBase();
                      
        }

        static public void RemoveUser(User user)
        {
            ExpediaUsers.Remove(user);
            UpdateUserDataBase();               
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


                char _Option = '0';
                while (_Option == '0')
                {
                    _Option = char.Parse(Console.ReadLine()); 
                    if (_Option == '3') break;
                    if (_Option != '1' && _Option != '2')
                    {
                        Console.WriteLine("Invalid Option\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                        char _NewOption;
                        _NewOption = char.Parse(Console.ReadLine());
                        if (_NewOption != '1') return;
                        else _Option = '0';
                    }
                    if (_Option == '0')
                        Console.Write("Try Again : ");
                }


                if (_Option == '1')
                {
                    int user_id = LogIn();
                    if (user_id != -1)
                    {
                        EnterSystem(ExpediaUsers[user_id]);
                    }                  
                }
                else if (_Option == '2')
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

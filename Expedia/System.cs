using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
namespace Expedia
{
    internal class System
    {

        const string USERS_FILE_PATH = @"D:\Projects\Projects\C#\Expedia\Expedia\DataBase\Users.txt";
       
        static private List <User> ExpediaUsers = new List <User> ();

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
                    ExpediaUsers.Add(HelperMethods.ConvertDataToUser(line));
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

        private int LogIn()
        {
            string email = HelperMethods.GetStringInput(HelperMethods.Message.MAIL_MESSAGE);
            Console.Write("Please enter your password: ");
            string password = HelperMethods.encryptedInput();
            password = HelperMethods.HashPassword(password);
            bool validUserDate = ExpediaUsers.Any(user => user.ValidEmailAndPassword(email, password));

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
                    validUserDate = ExpediaUsers.Any(user => user.ValidEmailAndPassword(email, password));
                }
                else
                {
                    return -1;
                }
            }

            // Return the id oftheuser

            return ExpediaUsers.FindIndex(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
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

            string dateTimeStringVersion = HelperMethods.GetDateTime();
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
            Session session = new Session(user);
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

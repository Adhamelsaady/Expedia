using System;
using System.Collections.Generic;

namespace Expedia
{
    internal class User
    {
        string _email, _firstName, _LastName, _phoneNumber, _passportId, _emergencyContact, _password;
        DateTime _birthDate;
        bool _male;
        int _systemId;
        List<int> _FlightsID;

        public string Email
        {
            get { return _email; }
        }

        public int ID
        {
            get { return _systemId; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }
        public User()
        {

        }

        public User(string email, string firstName, string LastName, string phoneNumber, string emergencyContact, DateTime birthDate, bool male, int systemID, string password)
        {
            _email = email;
            _firstName = firstName;
            _LastName = LastName;
            _phoneNumber = phoneNumber;
            _emergencyContact = emergencyContact;
            _birthDate = birthDate;
            _male = male;
            _systemId = systemID;
            _password = password;
            _FlightsID = new List<int>();
        }

        public string data()
        {
            string Data = "";
            Data += _systemId;
            Data += '|';
            Data += _email;
            Data += '|';
            Data += _firstName;
            Data += '|';
            Data += _LastName;
            Data += '|';
            Data += _phoneNumber;
            Data += '|';
            Data += _emergencyContact;
            Data += '|';
            Data += HelperMethods.DateTimeYearOnly(_birthDate);
            Data += '|';
            Data += _male;
            Data += '|';
            Data += _password;
            foreach (int FilghtIndex in _FlightsID)
            {
                Data += FilghtIndex.ToString();
                Data += "|";
            }
            return Data;
        }

        public bool ValidEmailAndPassword(string email, string password)
        {
            return email == _email && password == _password;
        }

        private string gender(bool male)
        {
            if (male == true) return "Male";
            else return "Female";
        }

        public void ShowData()
        {
            Console.WriteLine(
                $"Name {_firstName + ' ' + _LastName}\n" +
                $"Phone Number {_phoneNumber}\n" +
                $"Emergency Contact {_emergencyContact}\n" +
                $"Email {_email}\n" +
                $"Gender {gender(_male)}\n" +
                $"Birth-Date {HelperMethods.DateTimeYearOnly(_birthDate)}"               
                );
        }

        public void AddFlight(int FlightId)
        {
            _FlightsID.Add(FlightId);
        }

        public void MyFlightsHistory()
        {
            foreach (var flight in _FlightsID) Console.WriteLine(flight);
        }

        private void ChangePassword()
        {
            bool run = true;
            while (run)
            {
                Console.Write("Enter your old password: ");
                string OldPassword;
                OldPassword = HelperMethods.encryptedInput();
                if (this._password == HelperMethods.HashPassword(OldPassword))
                {
                    Console.WriteLine("Enter the new password : ");
                    string password = HelperMethods.GetPassword();
                    if (password != "!") this._password = password;
                    run = false;
                }
                else
                {
                    Console.WriteLine("Invalid Password");
                    Console.WriteLine(HelperMethods.Message.TRY_AGAIN_MESSAGE);
                    char option;
                    option = char.Parse(Console.ReadLine());
                    if (option != '1') run = false; 
                }
            }

        }

        private void ChangePhoneNumber()
        {
            string NewPhoneNumber = HelperMethods.GetValidatedInput(HelperMethods.Message.PHONE_NUMBER_MESSAGE,
                                                  number => HelperMethods.ValidatePhoneNumber(number),
                                                  "The phone number is invalid.\n"
                                               );
            if(NewPhoneNumber != "!")
                this._phoneNumber = NewPhoneNumber;
        }

        private void ChangeEmergencyNumber()
        {
            string NewEmergencyNumber = HelperMethods.GetValidatedInput(HelperMethods.Message.EMERGENCY_NUMBER_MESSAGE,
                                                       number => HelperMethods.ValidatePhoneNumber(number),
                                                       "The emergency number is invalid.\n"
                                                    );
            if (NewEmergencyNumber == "!") 
                this._emergencyContact = NewEmergencyNumber;
        }

        public void ModifyData()
        {
            Console.WriteLine("Enter number in range [1 - 4]: ");
            Console.WriteLine(HelperMethods.Message.CHANGEDATAMESSGE);

            int option;
            option = int.Parse(Console.ReadLine());
            if (option == 1) ChangePassword();
            else if (option == 2) ChangePhoneNumber();
            else if (option == 3) ChangeEmergencyNumber();
            else return;
        }

    }
}

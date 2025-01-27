using System;
using System.Collections.Generic;

namespace Expedia
{
    internal class User
    {
        string _email, _firstName, _LastName, _phoneNumber, _passportId, _emergencyContact, _password;
        DateTime _birthDate;
        bool _male;
        long _systemId;
        List<int> _relevantsIds;

        public string Email
        {
            get { return _email; }
        }

        public long ID
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

            return Data;
        }


        public bool isVaild(string email, string password)
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

    }
}

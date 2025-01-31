using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia
{
    internal class Session
    {
        User CurrentUser;
        public Session(User CurrentUser) 
        { 
            this.CurrentUser = CurrentUser;
            StartTheSession();
        }


        public void ViewProfile()
        {
            Console.WriteLine("To view data only choose 1 and to modify data choose 2");
            int option; 
            option = int.Parse(Console.ReadLine());
            if(option == 1)
            {
                CurrentUser.ShowData();
            } 
            else if(option == 2)
            {
                System.RemoveUser(CurrentUser);
                CurrentUser.ModifyData();
                System.AddUser(CurrentUser);
            }
            else
            {
                return;
            }
        
        }

        private void MainMenu() 
        {
            Console.WriteLine("Choose one of the following options: ");
            Console.WriteLine("1 : View Profile");
            Console.WriteLine("2 : Book a flight");
            Console.WriteLine("3 : Log out");
        }

        private void GetFlightSpecefications()
        {
            string From = HelperMethods.GetAirPort("from");
            if (From == "!") return;

            string To = HelperMethods.GetAirPort("to");
            if (To == "!") return;

            DateTime DefultDateTime = new DateTime(1, 1, 1);
            DateTime StartingDate = HelperMethods.GetFlightDate();
            if (StartingDate == DefultDateTime) return;

            DateTime EndingDate = HelperMethods.GetFlightDate();
            if (StartingDate == DefultDateTime) return;

            double FromPrice = double.Parse(Console.ReadLine());
            double ToPrice = double.Parse(Console.ReadLine());

            FlightsAPI.FlightsSearch(From , To , StartingDate , EndingDate , FromPrice , ToPrice);

        }

        private void BookFlight()
        {
            GetFlightSpecefications();
            Console.WriteLine("Enter the flight ID you want to book :");
            int FlightID = int.Parse(Console.ReadLine());

            // Book Flight for the user
        }

        private void StartTheSession()
        {
            bool run = true;
            Console.WriteLine($"Welcome To Expedia , {CurrentUser.FirstName}");
            while (run)
            {
                MainMenu();
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
                    ViewProfile();
                }
                else if (_Option == 2)
                {
                    BookFlight();
                }
                else
                {
                    run = false;
                }
            }
           
        }
    }
}

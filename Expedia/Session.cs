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

            }
            else
            {
                return;
            }
        
        }

        private void MainMenu() 
        {
            Console.WriteLine($"Welcome To Expedia , {CurrentUser.FirstName}");
            Console.WriteLine("Choose one of the following options: ");
            Console.WriteLine("1 : View Profile");
            Console.WriteLine("2 : Book a flight");
            Console.WriteLine("3 : Log out");
        }
        private void StartTheSession()
        {
            bool run = true;
            
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
                    Console.WriteLine("This is the flights HAHAHAHAHA");
                }
                else
                {
                    run = false;
                }
            }
           
        }
    }
}

using System;
using System.IO;

namespace Expedia
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Any input by the users starts with _
            System system = new System();

            foreach (var us in system._SystemUsers)
                Console.WriteLine(us.Email + " " + us.ID);

            Console.WriteLine("____________________________________");
            system.RunSystem();
            foreach (var us in system._SystemUsers)
                Console.WriteLine(us.Email + " " + us.ID);


        }
    }
}

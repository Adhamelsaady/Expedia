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

        public enum Country
        {
            USA = 0, UK = 1, China = 2, Japan = 3, UAE = 4, Germany = 5, France = 6, India = 7, Australia = 8,
            Canada = 9, Brazil = 10, SouthKorea = 11, Russia = 12, Singapore = 13, Turkey = 14, Thailand = 15,
            Netherlands = 16, HongKong = 17, Qatar = 18, SouthAfrica = 19, Egypt = 20
        }

        public enum MajorAirports
        {
            // USA
            ATL_HartsfieldJacksonAtlanta, LAX_LosAngeles, ORD_ChicagoOHare,

            // UK
            LHR_LondonHeathrow, LGW_LondonGatwick, MAN_Manchester,

            // China
            PEK_BeijingCapital, PVG_ShanghaiPudong, CAN_GuangzhouBaiyun,

            // Japan
            HND_TokyoHaneda, NRT_TokyoNarita, KIX_KansaiInternational,

            // UAE
            DXB_DubaiInternational, AUH_AbuDhabiInternational,

            // Germany
            FRA_Frankfurt, MUC_Munich, BER_BerlinBrandenburg,

            // France
            CDG_ParisCharlesDeGaulle, ORY_ParisOrly, NCE_NiceCoteDAzur,

            // India
            DEL_IndiraGandhi, BOM_ChhatrapatiShivaji, BLR_Kempegowda,

            // Australia
            SYD_Sydney, MEL_Melbourne, BNE_Brisbane,

            // Canada
            YYZ_TorontoPearson, YVR_Vancouver, YUL_MontrealTrudeau, YYC_CalgaryInternational,

            // Brazil
            GRU_SaoPauloGuarulhos, GIG_RioDeJaneiroGaleao, BSB_Brasilia,

            // South Korea
            ICN_Incheon, GMP_Gimpo, CJU_Jeju,

            // Russia
            SVO_Sheremetyevo, DME_Domodedovo, LED_Pulkovo,

            // Singapore
            SIN_Changi,

            // Turkey
            IST_Istanbul, SAW_SabihaGokcen,

            // Thailand
            BKK_BangkokSuvarnabhumi, DMK_DonMueang,

            // Netherlands
            AMS_AmsterdamSchiphol,

            // Hong Kong
            HKG_HongKongInternational,

            // Qatar
            DOH_HamadInternational,

            // South Africa
            JNB_ORTambo, CPT_CapeTownInternational,

            // Egypt
            CAI_CairoInternational, HRG_HurghadaInternational, SSH_SharmElSheikh
        }

        private static readonly Dictionary<Country, MajorAirports[]> airportsByCountry = new Dictionary<Country, MajorAirports[]>()
        {
            { Country.USA, new[] { MajorAirports.ATL_HartsfieldJacksonAtlanta, MajorAirports.LAX_LosAngeles, MajorAirports.ORD_ChicagoOHare } },
            { Country.UK, new[] { MajorAirports.LHR_LondonHeathrow, MajorAirports.LGW_LondonGatwick, MajorAirports.MAN_Manchester } },
            { Country.China, new[] { MajorAirports.PEK_BeijingCapital, MajorAirports.PVG_ShanghaiPudong, MajorAirports.CAN_GuangzhouBaiyun } },
            { Country.Japan, new[] { MajorAirports.HND_TokyoHaneda, MajorAirports.NRT_TokyoNarita, MajorAirports.KIX_KansaiInternational } },
            { Country.UAE, new[] { MajorAirports.DXB_DubaiInternational, MajorAirports.AUH_AbuDhabiInternational } },
            { Country.Germany, new[] { MajorAirports.FRA_Frankfurt, MajorAirports.MUC_Munich, MajorAirports.BER_BerlinBrandenburg } },
            { Country.France, new[] { MajorAirports.CDG_ParisCharlesDeGaulle, MajorAirports.ORY_ParisOrly, MajorAirports.NCE_NiceCoteDAzur } },
            { Country.India, new[] { MajorAirports.DEL_IndiraGandhi, MajorAirports.BOM_ChhatrapatiShivaji, MajorAirports.BLR_Kempegowda } },
            { Country.Australia, new[] { MajorAirports.SYD_Sydney, MajorAirports.MEL_Melbourne, MajorAirports.BNE_Brisbane } },
            { Country.Canada, new[] { MajorAirports.YYZ_TorontoPearson, MajorAirports.YVR_Vancouver, MajorAirports.YUL_MontrealTrudeau, MajorAirports.YYC_CalgaryInternational } },
            { Country.Brazil, new[] { MajorAirports.GRU_SaoPauloGuarulhos, MajorAirports.GIG_RioDeJaneiroGaleao, MajorAirports.BSB_Brasilia } },
            { Country.SouthKorea, new[] { MajorAirports.ICN_Incheon, MajorAirports.GMP_Gimpo, MajorAirports.CJU_Jeju } },
            { Country.Russia, new[] { MajorAirports.SVO_Sheremetyevo, MajorAirports.DME_Domodedovo, MajorAirports.LED_Pulkovo } },
            { Country.Singapore, new[] { MajorAirports.SIN_Changi } },
            { Country.Turkey, new[] { MajorAirports.IST_Istanbul, MajorAirports.SAW_SabihaGokcen } },
            { Country.Thailand, new[] { MajorAirports.BKK_BangkokSuvarnabhumi, MajorAirports.DMK_DonMueang } },
            { Country.Netherlands, new[] { MajorAirports.AMS_AmsterdamSchiphol } },
            { Country.HongKong, new[] { MajorAirports.HKG_HongKongInternational } },
            { Country.Qatar, new[] { MajorAirports.DOH_HamadInternational } },
            { Country.SouthAfrica, new[] { MajorAirports.JNB_ORTambo, MajorAirports.CPT_CapeTownInternational } },
            { Country.Egypt, new[] { MajorAirports.CAI_CairoInternational, MajorAirports.HRG_HurghadaInternational, MajorAirports.SSH_SharmElSheikh } }
        };

        public static string GetDateTime()
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
            return DateTime.TryParse(DateAndTime, out dateTime);

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

        private static void Printcountries()
        {
            foreach (var country in Enum.GetValues(typeof (Country)))
            {
                Console.WriteLine($"{(int)country + 1} - {country.ToString()}.");
            }
        }
        
        private static void PrintAirPorts(Country country)
        {
            var CountyAirPorts = airportsByCountry[country];
            for (int i = 0; i < CountyAirPorts.Length; ++i)
            {
                Console.WriteLine($"{i + 1} - {CountyAirPorts[i].ToString()}");
            }
        }

        public static string GetAirPort(string direction)
        {
            Console.WriteLine($"Enter the number of the country you are traveling {direction} : ");
            Printcountries();
            int option;
            option = int.Parse(Console.ReadLine());
            if(option > 21)  return "!"; // invalid option

            Console.WriteLine($"Enter the Airport you are traveling {direction}");
            var country = (Country)Enum.ToObject(typeof (Country) , --option);
            PrintAirPorts(country);

            option = int.Parse(Console.ReadLine());
            if (option > airportsByCountry[country].Length) return "!"; // invalid option
            string AirPort = airportsByCountry[country][--option].ToString();
            return AirPort;
        }

        public static DateTime GetFlightDate()
        {
            // This method is responsable for getting an input in the form of a data [MM/DD/YYYY]
            DateTime FlightDate = new DateTime(1 , 1 , 1);
            string message = "Enter the starting range of the date of the flight in the form of  [MM/DD/YYYY]:";
            string dateTimeInput = HelperMethods.GetStringInput(message);
            bool isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
            if (isValidDateTime)
            {
                string[] DateParsing = dateTimeInput.Split('/');
                int month = int.Parse(DateParsing[0]);
                int day = int.Parse(DateParsing[1]);
                int year = int.Parse(DateParsing[2]);
                FlightDate = new DateTime(year , month , day);
                Console.WriteLine("We are here");
            }
            while (!isValidDateTime)
            {
                Console.WriteLine("The date are invalid.\n" + HelperMethods.Message.TRY_AGAIN_MESSAGE);
                char _Option;
                _Option = char.Parse(Console.ReadLine());
                if (_Option == '1')
                {

                    dateTimeInput = HelperMethods.GetStringInput(message);
                    isValidDateTime = HelperMethods.ValidateDateAndTime(dateTimeInput);
                    string[] DateParsing = dateTimeInput.Split('/');
                    int month = int.Parse(DateParsing[0]);
                    int day = int.Parse(DateParsing[1]);
                    int year = int.Parse(DateParsing[2]);
                    FlightDate = new DateTime(year , month , day);
                }
                else
                {
                    return FlightDate;
                }
            }

            return FlightDate;
        }

        public static DateTime StringToDate(string Date)
        {
            string[] parse = Date.Split('/');
            DateTime ret = new DateTime(int.Parse(parse[2]), int.Parse(parse[0]), int.Parse(parse[1]));
            return ret;
        }

    }
}

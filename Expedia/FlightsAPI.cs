using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia
{
    internal class FlightsAPI
    {

        class Flight
        {
            private string _From, _To , _Compnay;
            private DateTime _TakingOffAt;
            private double _Price, _CancellationFee;
            private int _AvailableSeats , _Id;

            Flight() {            }
            public Flight(int id , string From , string To , string Company , DateTime TakingOff , double Price , double CancellationFee , int AvailableSeats)
            {
                _Id = id;
                _From = From;
                _To = To;
                _Compnay = Company;
                _Price = Price;
                _CancellationFee = CancellationFee;
                _TakingOffAt = TakingOff;
                _AvailableSeats = AvailableSeats;
            }

            public string From { get; private set; }
            public string To { get; private set; }
            public string Company { get; private set; }
            public DateTime TakingOffAt { get; private set; }
            public double Price { get; private set; }
            public double CancellationFee { get; private set; }
            public int NumberOfAvailableSeats { get; private set; }
            public void DecrementSeats() { _AvailableSeats--; }

            public override string ToString()
            {
                return $"From: {_From}    To: {_To}    Date: {_TakingOffAt}    Company : {_Compnay}    Price: {_Price}    Cancellation Fee {_CancellationFee}";
            }
        }

        static List <Flight> _Flights = new List <Flight> ();
        const string FLIGHTS_DATA_PAHT = @"D:\Projects\Projects\C#\Expedia\Expedia\DataBase\FlightsDataBase.txt";


        public static void priiiint()
        {
            foreach (var f in _Flights) Console.WriteLine(f.ToString());
        }
        public FlightsAPI() { }

        public static void LoadFlightsDataBase()
        {
            if (_Flights.Count != 0) _Flights.Clear();
            using (StreamReader reader = new StreamReader(FLIGHTS_DATA_PAHT))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _Flights.Add(StringToFlight(line));
                }
            }
        }

        private static void UpdataFlightsDataBae()
        {

        }

        private static Flight StringToFlight(string line)
        {
            string[] data = line.Split('|');
            Flight flight = new Flight(
                int.Parse(data[0]) , // id
                data[1] , // from
                data[2] ,  // to
                data[4], // company
                HelperMethods.StringToDate(data[5]), // Date
                double.Parse(data[3]) , // price
                double.Parse(data[7]) , // Cancellation Fee
                int.Parse(data[6]) // Seats
                );
            return flight;
        }
        public static string FlightsSearch(string FromAirPort , string ToAirPort , DateTime StartingDate , DateTime EndingDate , double FromPrice, double ToPrice) 
        {
            return _Flights.Where(flight =>
                flight.From == FromAirPort && flight.To == ToAirPort &&
                flight.TakingOffAt >= StartingDate && flight.TakingOffAt <= EndingDate &&
                flight.Price >= FromPrice && flight.Price <= ToPrice
            ).ToString(); 
        }

        public static string GetFlightDetails(int FlightId)
        {
            return _Flights[FlightId].ToString();
        }

      
    }
}

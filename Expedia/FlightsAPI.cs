using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia
{
    internal class FlightsAPI
    {

        class Flight
        {
            private string _From, _to;
            private DateTime _TakingOff;
            private double _Price, _Rate, _CancellationFee;
            private int _NumberOfAvailableSeats;
        }

        List <Flight> _Flights = new List <Flight> ();
        const string FLIGHTS_DATA_PAHT = @"D:\Projects\Projects\C#\Expedia\Expedia\DataBase\FlightsDataBase.txt";

        public FlightsAPI() { }

        private void LoadFlightsDataBase()
        {

        }

        private void UpdataFlightsDataBae()
        {

        }

        public void FlightsSearch() // void for now
        {

        }

        public void BookFlightForUser(int UserId , int FlightId)
        {

        }

        public void GetFlightDetails(int FlightId)
        {   

        }

        public void RateFlight(int FlightId , double Rate)
        {

        }

    }
}

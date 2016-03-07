using System.Collections.Generic;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        void AddStop(Stop newStop,string username,  string theTrip);
        IEnumerable<Trip> GetUserAllTripsWithStops(string name);
        Trip GetTripByName(string tripName, string name);
    }
}
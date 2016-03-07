using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void AddStop(Stop newStop, string username, string tripName)
        {
            var theTrip = GetTripByName(tripName, username);
            theTrip?.Stops.Add(newStop);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            _context.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }catch(Exception ex)
            {
                _logger.LogError("Could not get trips from Database",ex);
                return null;
            }
        }
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            { return _context.Trips
                .Include(t =>t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
        }catch(Exception ex)
            {
                _logger.LogError("Could not get trips from Database",ex);
                return null;
            }
}


        public Trip GetTripByName(string tripName, string name)
        {
            return _context.Trips.Include(t => t.Stops)
               .Where(t => t.Name == tripName && t.UserName == name)
               .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserAllTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
                  .Include(t => t.Stops)
                  .OrderBy(t => t.Name)
                  .Where(t=> t.UserName == name)
                  .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from Database", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

using Domain.DTO.UIDtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.UIServices.AuctionService
{
    public class TripsService : ITripsService
    {
        private readonly VODContext _db;

        public TripsService(VODContext db)
        {
            _db = db;
        }
        public List<TripsDto> GetTrips(string userId = null)
        {
            var trips = _db.Trips
                .Where(r => r.IsDeleted == false)
                  .Include(t => t.Driver)
                  .Include(t => t.Route)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DriverName = a.Driver.Name,
                    PlateNumber = a.Driver.PlateNumber,
                    Drivernumber = a.Driver.PhoneNumber,
                    DriverProfileImageUrl = a.Driver.ProfileImageUrl,
                    RouteName = a.Route.Name,
                    NoOfSeats = a.NoOfSeats,
                    Date = a.Date,
                    Price = a.Route.Price,
                    IsBooked = userId != null && a.Reservations.Any(r => r.UserId == userId),
                    Reservations = a.Reservations.Select(r => new TripReservation
                    {
                        UserId = r.UserId,


                    }).ToList()

                })
                .ToList();

            return trips;
        }

        public async Task<bool> ReserveTrip(int tripId, string userId)
        {
            var alreadyReserved = await _db.TripReservations
                .AnyAsync(r => r.TripId == tripId && r.UserId == userId);
            if (alreadyReserved) return false;

            // Get the trip and its current reservation count
            var trip = await _db.Trips
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null) return false;

            var currentReservations = trip.Reservations.Count;

            if (currentReservations >= trip.NoOfSeats) return false;

            var reservation = new TripReservation
            {
                TripId = tripId,
                UserId = userId
            };

            _db.TripReservations.Add(reservation);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> Cancel(int tripId, string userId)
        {
            var reservation = await _db.TripReservations
                .FirstOrDefaultAsync(r => r.TripId == tripId && r.UserId == userId);

            if (reservation == null) return false;

            _db.TripReservations.Remove(reservation);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}

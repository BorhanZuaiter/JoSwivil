namespace Domain.Entities
{
    public class TripReservation
    {
        public int TripId { get; set; }
        public Trips Trips { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime ReservedOn { get; set; }
    }
    public class TripReservationDto
    {
        public int TripId { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime ReservedOn { get; set; }
    }
}

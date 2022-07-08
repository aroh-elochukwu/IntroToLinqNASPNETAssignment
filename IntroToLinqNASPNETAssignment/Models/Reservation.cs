namespace IntroToLinqNASPNETAssignment.Models
{
    public class Reservation
    {
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public int Id { get; set; } = 0;
        public int Occupants { get; set; }
        public bool IsCurrent { get; set; }
        public Client Client { get; set; } 
        public Room Room { get; set; } 
        public Reservation( int occupants, Client client, Room room, DateTime startDate)
        {
            Created = DateTime.Now;
            Id = Id++;
            Occupants = occupants;
            Client = client;
            Room = room;
            StartDate = startDate;
        }


    }
}

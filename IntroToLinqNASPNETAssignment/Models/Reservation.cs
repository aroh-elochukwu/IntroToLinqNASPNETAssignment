namespace IntroToLinqNASPNETAssignment.Models
{
    public class Reservation
    {
        public DateTime DateTime { get; set; }
        public int Id { get; set; } = 0;
        public int Occupants { get; set; }
        public bool IsCurrent { get; set; }
        public Client Client { get; set; } 
        public Room Room { get; set; } 
        public Reservation(DateTime dateTime, int occupants, Client client, Room room)
        {
            DateTime = dateTime;
            Id = Id++;
            Occupants = occupants;
            Client = client;
            Room = room;
        }

        
    }
}

namespace IntroToLinqNASPNETAssignment.Models
{
    public class Room
    {
        public int Id { get; set; } = 0;
        public string Number { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
        public ICollection<Reservation> Reservations { get; set; }= new List<Reservation>();
        public Room(string number, int capacity)
        {
            Id = Id++;
            Number = number;
            Capacity = capacity;        
        }

        
    }
}

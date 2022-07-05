namespace IntroToLinqNASPNETAssignment.Models
{
    public class Client
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public long CreditCard { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public Client(string name, long creditCard)
        {
            Name = name;
            CreditCard = creditCard;

        }


        
    }

    
}

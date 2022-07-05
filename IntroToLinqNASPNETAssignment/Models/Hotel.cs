namespace IntroToLinqNASPNETAssignment.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;

        }

        public Client GetClient(int clientID)
        {
            try
            {
                return Clients.First(theClient => theClient.Id == clientID);

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public Reservation GetReservation(int ID)
        {
            try
            {
                return Reservations.First(theReservation => theReservation.Id == ID);

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public Room GetRoom(string roomNumber)
        {
            try
            {
                return Rooms.First(theRoom => theRoom.Number == roomNumber);

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        // returning a hashset of room in the function below creates a bug...discuss with Zack
        public List<Room> GetVacantRooms()
        {
            try
            {
                return Rooms.Where(choosenRoom => choosenRoom.IsOccupied != true).ToList();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public List<Client> TopThreeClients()
        {
            List<Client> sortedClientList = Clients.OrderBy(presentClient => presentClient.Reservations.Count).ToList();


            try
            {
                return sortedClientList.Take(3).ToList();
            }
            catch (NullReferenceException)
            {
                return null;
            }

        }

        public Reservation AutomaticReservation(int clientID, int occupants)
        {
            List<Room> availableRooms = Rooms.Where(presentRoom => presentRoom.IsOccupied == false).ToList();
            Client client  = GetClient(clientID);
            try
            {
                Room roomChoice = availableRooms.First(room => room.Capacity >= occupants);
                Reservation newReservation = new Reservation(DateTime.Now, occupants, client, roomChoice);
                Reservations.Add(newReservation);
                return newReservation;

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}


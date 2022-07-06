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

        public Room GetRoom(int roomNumber)
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
                Reservation newReservation = new Reservation(DateTime.Now, occupants, client, roomChoice, DateTime.Today);
                Reservations.Add(newReservation);
                return newReservation;

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public Reservation ReserveRoom(int clientID, int occupants, int roomNumber, DateTime reservationDate)
        {           
            Client client = GetClient(clientID);

            try
            {
                Room roomChoice = Rooms.First(room => room.Number == roomNumber && room.Capacity >= occupants);
                Reservation newReservation = new Reservation(DateTime.Now, occupants, client, roomChoice, reservationDate);
                Reservations.Add(newReservation);
                return newReservation;

            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public void CheckIn(string clientName)
        {
            try
            {
                Client presentClient = Clients.First(theClient => theClient.Name == clientName);

                Reservation clientReservation = presentClient.Reservations.First(theReservation => theReservation.StartDate == DateTime.Today);
                clientReservation.Room.IsOccupied = true;                
            }
            catch(NullReferenceException)
            {
                
            }
        }

        public void CheckOutRoom(int roomNumber)
        {
            try
            {
               Room theRoom = Rooms.First(room => room.Number == roomNumber);
                theRoom.IsOccupied = false;
            }
            catch(NullReferenceException)
            {

            }
        }

        public void CheckOutRoom(string clientName)
        {
            try
            {
                Client presentClient = Clients.First(theClient => theClient.Name == clientName);
                Reservation clientReservation = presentClient.Reservations.First(theReservation => theReservation.StartDate == DateTime.Today);
                clientReservation.Room.IsOccupied = false;

            }
            catch (NullReferenceException)
            {

            }

        }

        //I am having difficulty agreeing with the questions description of a room's occupancy
        //I think TotalCapacityRemaining() looks better like this
        public int TotalCapacityRemaining()
        {
            try
            {
               return Rooms.Where(room => room.IsOccupied == false).ToList().Count();

            }
            catch (NullReferenceException)
            {
                return 0;
            }  

        }

        public int AverageOccupancyPercentage()
        {
            try
            {
               List<Room> occupiedRooms = Rooms.Where(room => room.IsOccupied == true).ToList();
                int presentOccupants = 0;
                int occupiedRoomsFullCapacity = 0;

                foreach(Room room in occupiedRooms)
                {
                    presentOccupants =+ room.Reservations.First(reservation => reservation.StartDate == DateTime.Today).Occupants;

                    occupiedRoomsFullCapacity = +occupiedRooms.Capacity;
                }

                return (presentOccupants * 100 )/ occupiedRoomsFullCapacity ;

            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        public List<Reservation> FutureBookings()
        {
           
            try
            {
                return Reservations.Where(reservation => reservation.StartDate > DateTime.Today).ToList();
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }


    }
}


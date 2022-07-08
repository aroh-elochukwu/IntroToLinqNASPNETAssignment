namespace IntroToLinqNASPNETAssignment.Models
{
    public static class Hotel
    {
        public static int Id { get; set; } = 1;
        public static string Name { get; set; } = "Eko Atlantic";
        public static string Address { get; set; } = "Pearls Atlantic City, Eko Blvd, Victoria Island 106104, Lagos, Nigeria";
        public static ICollection<Room> Rooms { get; set; } = new List<Room>();
        public static ICollection<Client> Clients { get; set; } = new List<Client>();
        public static ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public static void AddRoom(int number , int capacity)
        {           
            Rooms.Add(new Room(number,capacity));
        }

        public static void RegisterClient(string name, long creditCard)
        {
            Clients.Add(new Client(name, creditCard));
        }
        public static Client GetClient(int clientID)
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

        public static Reservation GetReservation(int ID)
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

        public static Room GetRoom(int roomNumber)
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
        public static List<Room> GetVacantRooms()
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

        public static List<Client> TopThreeClients()
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

        public static Reservation AutomaticReservation(int clientID, int occupants)
        {
            List<Room> availableRooms = Rooms.Where(presentRoom => presentRoom.IsOccupied == false).ToList();
            Client client  = GetClient(clientID);
            try
            {
                Room roomChoice = availableRooms.First(room => room.Capacity >= occupants);
                Reservation newReservation = new Reservation( occupants, client, roomChoice, DateTime.Today);
                Reservations.Add(newReservation);
                return newReservation;

            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static Reservation ReserveRoom(int clientID, int occupants, int roomNumber, DateTime reservationDate)
        {           
            Client client = GetClient(clientID);

            try
            {
                Room roomChoice = Rooms.First(room => room.Number == roomNumber && room.Capacity >= occupants);
                Reservation newReservation = new Reservation( occupants, client, roomChoice, reservationDate);
                Reservations.Add(newReservation);
                return newReservation;

            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public static void CheckIn(string clientName)
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

        public static void CheckOutRoom(int roomNumber)
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

        public static void CheckOutRoom(string clientName)
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
        public static int TotalCapacityRemaining()
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

        public static int AverageOccupancyPercentage()
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

        public static List<Reservation> FutureBookings()
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

        static Hotel()
        {
            AddRoom(101, 2);
            AddRoom(102, 2);
            AddRoom(103, 3);
            AddRoom(104, 3);
            AddRoom(105, 2);
            AddRoom(106, 4);
            AddRoom(107, 4);
            AddRoom(108, 2);
            AddRoom(109, 2);
            
            RegisterClient("Murphy", 0987000098459874);
            RegisterClient("Imelda", 7353743674583255);
            RegisterClient("Chimamanda", 0934618309741129);
            RegisterClient("Santana", 8730983223096352);
            RegisterClient("Yobo", 9764091256433398);

            DateTime independenceDay = new DateTime(2022, 7, 1);
            DateTime chritmasDay = new DateTime(2022, 12, 25);
            /*
            ReserveRoom(2, 1, 108, independenceDay);
            ReserveRoom(3, 1, 108, chritmasDay);
            ReserveRoom(4,2, 103, DateTime.Now);

            AutomaticReservation(1, 1);
            AutomaticReservation(3, 1);
            AutomaticReservation(5, 1);

            CheckIn("Santana");
            CheckIn("Yobo");

            CheckOutRoom(103);
            */
            



        }


    }
}


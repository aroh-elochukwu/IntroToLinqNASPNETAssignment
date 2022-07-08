using Microsoft.AspNetCore.Mvc;
using IntroToLinqNASPNETAssignment.Models;

namespace IntroToLinqNASPNETAssignment.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View(Hotel.Rooms);
        }
    }
}

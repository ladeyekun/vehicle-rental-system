using Microsoft.AspNetCore.Mvc;

namespace Vehicle_Rental_System.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

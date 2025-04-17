using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: /Location
        public IActionResult Index()
        {
            var locations = _locationService.GetAllLocations();
            return View(locations);
        }

        // GET: /Location/Details/5
        public IActionResult Details(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // GET: /Location/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            _locationService.CreateLocation(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Location/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // POST: /Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            _locationService.UpdateLocation(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Location/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // POST: /Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _locationService.DeleteLocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

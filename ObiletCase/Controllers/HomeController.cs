using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.Controllers.Base;
using ObiletCase.Models;
using ObiletCase.Models.ViewModels;
using ObiletCase.Services;

namespace ObiletCase.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IObiletApiService obiletService)
            : base(obiletService)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var session = await GetValidSessionAsync();
            var locations = await _obiletService.GetBusLocationsAsync(session);

            var model = new IndexViewModel
            {
                Session = session,
                Locations = locations,
                DepartureDate = DateTime.Today.AddDays(1)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(IndexViewModel model)
        {
            if (model.SelectedOriginId == model.SelectedDestinationId)
                ModelState.AddModelError("", "Kalkış ve varış noktaları aynı olamaz.");

            if (model.DepartureDate < DateTime.Today)
                ModelState.AddModelError("", "Geçmiş bir tarih seçilemez.");

            if (!ModelState.IsValid) // model dooğrulama kurallarına uygun mu ? modelstate'e eklenen hatalar var mı?
            {
                model.Locations = await _obiletService.GetBusLocationsAsync(model.Session);
                return View("Index", model);
            }

            return RedirectToAction("Index", "Journey", new // bu sayfada arama yapıldıktan sonra JourneyController'daki Index metoduna yönlendiriyoruz.
            {
                originId = model.SelectedOriginId,
                destinationId = model.SelectedDestinationId,
                departureDate = model.DepartureDate.ToString("yyyy-MM-dd")
            });
        }

        [Route("api/locations")]
        [HttpGet]
        public async Task<IActionResult> GetLocations(string query) // Bu metod arama kutusuna yazılan sorguya göre lokasyonları filtreler.
        {
            var session = await GetValidSessionAsync();
            var allLocations = await _obiletService.GetBusLocationsAsync(session);

            var normalizedQuery = query.ToLower(new CultureInfo("tr-TR"));

            var matches = allLocations
                .Where(x => x.Name.ToLower(new CultureInfo("tr-TR")).Contains(normalizedQuery)) // örneğin İzmir aranırken culture bilgisi ile küçük harfe çeviriyoruz.
                .Select(x => new { label = x.Name, id = x.Id })
                .ToList();

            return Json(matches);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

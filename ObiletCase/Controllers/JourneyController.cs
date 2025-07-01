using Microsoft.AspNetCore.Mvc;
using ObiletCase.Controllers.Base;
using ObiletCase.Services;

namespace ObiletCase.Controllers
{
    public class JourneyController : BaseController
    {
        public JourneyController(IObiletApiService obiletService)
            : base(obiletService)
        {
        }

        public async Task<IActionResult> Index(int originId, int destinationId, string departureDate)
        {
            var session = await GetValidSessionAsync();
            var journeys = await _obiletService.GetJourneysAsync(session, originId, destinationId, DateTime.Parse(departureDate));
            journeys = journeys.OrderBy(j => j.Journey.Departure).ToList(); // Yolculukları kalkış saatine göre sıralıyoruz.

            return View(journeys);
        }
    }
}

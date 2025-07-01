using Microsoft.AspNetCore.Mvc;
using ObiletCase.Infrastructure.Helpers;
using ObiletCase.Models.Api.Session;
using ObiletCase.Services;

namespace ObiletCase.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IObiletApiService _obiletService;

        public BaseController(IObiletApiService obiletService)
        {
            _obiletService = obiletService;
        }

        protected async Task<SessionData> GetValidSessionAsync() //Burada sessionı kontrol edip, geçerli bir session döndürüyoruz.
        {
            var session = SessionManager.Get(HttpContext);

            if (session == null)
            {
                session = await _obiletService.GetSessionAsync();
                SessionManager.Save(HttpContext, session);
            }

            return session;
        }
    }
}

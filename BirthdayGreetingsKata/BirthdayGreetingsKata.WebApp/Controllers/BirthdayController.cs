using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayGreetingsKata.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthdayController : ControllerBase
    {
        readonly BirthdayService service;

        public BirthdayController(BirthdayService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader(Name = "X-App-Today")] DateTime? requestToday)
        {
            var today = requestToday ?? DateTime.Today;
            await service.SendGreetings(today);
            return NoContent();
        }
    }
}

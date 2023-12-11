using EPRN.Portal.Services.Interfaces;
using EPRN.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPRN.Portal.Controllers
{
    [Route("journey")]
    public class JourneyController : Controller
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpGet]
        [Route("{journeyId}/waste/subWasteType")]
        public async Task<IActionResult> SubWasteType(int journeyId)
        {
            JourneySubWasteTypesViewModel vm = await _journeyService.GetJourneySubWasteRequest(journeyId);
            return View(vm);
        }
    }
}

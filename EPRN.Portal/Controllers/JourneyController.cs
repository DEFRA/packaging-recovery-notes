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
        [Route("{journeyId}/subWasteType")]
        public async Task<IActionResult> SubWasteType(int journeyId)
        {
            JourneySubWasteTypesViewModel vm = await _journeyService.GetJourneySubWaste(journeyId);
            return View(vm);
        }

        [HttpPost]
        [Route("{journeyId}/subWasteType")]
        public async Task<IActionResult> SubWasteType(JourneySubWasteTypesViewModel vm)
        {
            if (vm == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return await SubWasteType(vm.JourneyId);

            await _journeyService.SaveJourneySubWaste(vm);

            return RedirectToAction("Index", "Home");
        }
    }
}

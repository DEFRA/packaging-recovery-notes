using EPRN.Portal.Services.Interfaces;
using EPRN.Portal.ViewModels;

namespace EPRN.Portal.Services
{
    public class JourneyService : IJourneyService
    {
        public async Task<JourneySubWasteTypesViewModel> GetJourneySubWasteRequest(int journeyId)
        {
            var vm = new JourneySubWasteTypesViewModel();
            vm.JourneyId = journeyId;
            vm.SubWasteTypes = new Dictionary<int, string>
            {
                { 1, "Type1" },
                { 2, "Type2" }
            };

            return vm;
        }
    }
}

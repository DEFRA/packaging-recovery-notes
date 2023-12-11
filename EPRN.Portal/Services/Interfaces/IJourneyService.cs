using EPRN.Portal.ViewModels;

namespace EPRN.Portal.Services.Interfaces
{
    public interface IJourneyService
    {
        Task<JourneySubWasteTypesViewModel> GetJourneySubWaste(int journeyId);
        Task SaveJourneySubWaste(JourneySubWasteTypesViewModel model);
    }
}

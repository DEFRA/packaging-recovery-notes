using EPRN.Portal.ViewModels;

namespace EPRN.Portal.Services.Interfaces
{
    public interface IJourneyService
    {
        Task<JourneySubWasteTypesViewModel> GetJourneySubWasteRequest(int journeyId);
    }
}

using AutoMapper;
using EPRN.Portal.Helpers;
using EPRN.Portal.Helpers.Interfaces;
using EPRN.Portal.Resources;
using EPRN.Portal.RESTServices.Interfaces;
using EPRN.Portal.Services.Interfaces;
using EPRN.Portal.ViewModels;

namespace EPRN.Portal.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IMapper _mapper;
        private readonly IHttpWasteService _httpWasteService;
        private readonly IHttpJourneyService _httpJourneyService;

        public JourneyService(
            IMapper mapper,
            IHttpWasteService httpWasteService,
            IHttpJourneyService httpJourneyService,
            ILocalizationHelper<WhichQuarterResources> localizationHelper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpWasteService = httpWasteService ?? throw new ArgumentNullException(nameof(httpWasteService));
            _httpJourneyService = httpJourneyService ?? throw new ArgumentNullException(nameof(_httpJourneyService));
        }

        public async Task<JourneySubWasteTypesViewModel> GetJourneySubWaste(int journeyId)
        {        
            var vm = new JourneySubWasteTypesViewModel();
            vm.JourneyId = journeyId;
            vm.SubWasteTypes = new Dictionary<int, string>
            {
                { 1, "Type1" },
                { 2, "Type2" },
                { 3, "other" }
            };

            return vm;
        }

        public Task<JourneySubWasteTypesViewModel> SaveJourneySubWaste(JourneySubWasteTypesViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

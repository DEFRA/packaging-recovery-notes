using AutoMapper;
using EPRN.Common.Dtos;
using EPRN.Portal.Helpers;
using EPRN.Portal.Helpers.Interfaces;
using EPRN.Portal.Resources;
using EPRN.Portal.RESTServices.Interfaces;
using EPRN.Portal.Services.Interfaces;
using EPRN.Portal.ViewModels;
using NuGet.Packaging;

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

        #region journey sub waste type

        public async Task<JourneySubWasteTypesViewModel> GetJourneySubWaste(int journeyId)
        {
            var vm = new JourneySubWasteTypesViewModel { JourneyId = journeyId };

            // get currently selected waste type for this journey
            JourneyWasteTypeDto journeyWasteTypeDto = await _httpJourneyService.GetJourneyWasteTypeDto(journeyId);
            if (journeyWasteTypeDto == null)
                throw new Exception("No waste type selected for this journey");

            // get collection of un-selected sub-waste types (lookups) based on the journey selected waste type
            vm.SubWasteTypes.AddRange(await GetJourneyWasteSubTypes(journeyWasteTypeDto.Id));

            // get currently selected waste sub-type for this journey
            JourneyWasteSubTypeDto journeyWasteSubTypeDto = await _httpJourneyService.GetJourneyWasteSubTypeDto(journeyId);
            if (journeyWasteSubTypeDto == null)
                return vm;
            else
            {
                vm.Adjustment = journeyWasteSubTypeDto?.Adjustment;
                var matchingSubWasteType = vm.SubWasteTypes.SingleOrDefault(x => x.Id == journeyWasteSubTypeDto.Id);
                if(matchingSubWasteType != null)
                    matchingSubWasteType.IsSelected = true;
            }

            return vm;
        }

        private async Task<IEnumerable<JourneySubWasteTypeViewModel>> GetJourneyWasteSubTypes(int journeyWasteTypeId)
        {
            var subWasteTypes = new List<JourneySubWasteTypeViewModel>();

            var wasteSubTypeDtos = await _httpWasteService.GetWasteSubTypes(journeyWasteTypeId);
            if (wasteSubTypeDtos == null || !wasteSubTypeDtos.Any())
                return null;

            subWasteTypes.AddRange(wasteSubTypeDtos.Select(x => new JourneySubWasteTypeViewModel
            {
                Adjustment = (double)x.Adjustment,
                Id = x.Id,
                IsSelected = false,
                Name = x.Name
            }));

            return subWasteTypes;
        }

        public async Task SaveJourneySubWaste(JourneySubWasteTypesViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            if (vm.SelectedSubWasteTypeId == null)
                throw new ArgumentNullException(nameof(vm.SelectedSubWasteTypeId));

            await _httpJourneyService.SaveSelectedSubWasteType(
                vm.JourneyId,
                vm.SelectedSubWasteTypeId.Value,
                vm.Adjustment);
        }

        #endregion

    }
}

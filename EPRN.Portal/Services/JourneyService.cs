using AutoMapper;
using EPRN.Common.Dtos;
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

        #region journey sub waste type


        /// <summary>
        /// Get currently selected waste type.
        /// If it exists then get relevant un-selected list of sub-wast types
        /// Assign selected sub-waste type, if selected
        /// Assign adjustment, if [other] sub-waste type selected
        /// </summary>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        public async Task<JourneySubWasteTypesViewModel> GetJourneySubWaste(int journeyId)
        {
            var vm = new JourneySubWasteTypesViewModel { JourneyId = journeyId };

            // get currently selected waste type for this journey
            JourneyWasteTypeDto journeyWasteTypeDto = await _httpJourneyService.GetJourneyWasteTypeDto(journeyId);
            if (journeyWasteTypeDto == null)
                return vm;

            // get currently selected waste sub-type for this journey
            JourneyWasteSubTypeDto journeyWasteSubTypeDto = await _httpJourneyService.GetJourneyWasteSubTypeDto(journeyId);
            if (journeyWasteTypeDto == null)
                return vm;

            vm.Adjustment = journeyWasteSubTypeDto.Adjustment;

            // get collection of un-selected sub-waste types (lookups) based on the journey selected waste type
            var wasteSubTypeDtos = await _httpWasteService.GetWasteSubTypes(journeyWasteTypeDto.Id);
            if (wasteSubTypeDtos == null || !wasteSubTypeDtos.Any())
                return vm;
            else
            {
                foreach (var wasteSubTypeDto in wasteSubTypeDtos)
                {
                    vm.SubWasteTypes.Add(new (wasteSubTypeDto.Id, wasteSubTypeDto.Name, wasteSubTypeDto.Id == journeyWasteSubTypeDto.Id));
                }
            }

            return vm;
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

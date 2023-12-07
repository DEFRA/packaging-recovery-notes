using EPRN.Common.Dtos;
using EPRN.Common.Enums;

namespace Waste.API.Services.Interfaces
{
    public interface IWasteService
    {

        Task<int> CreateJourney();

        Task SaveSelectedMonth(int journeyId, int selectedMonth);

        Task<DoneWaste> GetWhatHaveYouDoneWaste(int journeyId);

        Task SaveWhatHaveYouDoneWaste(int journeyId, DoneWaste whatHaveYouDoneWaste);


        Task<IEnumerable<WasteTypeDto>> GetWasteTypes();

        Task<WasteTypeDto> GetWasteType(int wasteTypeId);

        Task<string> GetWasteTypeName(int journeyId);

        Task SaveWasteType(int journeyId, int wasteTypeId);

        Task SaveWasteSubType(int journeyId, int wasteSubTypeId);


        Task<WasteRecordStatusDto> GetWasteRecordStatus(int journeyId);

        Task SaveTonnage(int journeyId, double tonnage);
        
        Task SaveBaledWithWire(int journeyId, bool baledWithWire);
    }
}

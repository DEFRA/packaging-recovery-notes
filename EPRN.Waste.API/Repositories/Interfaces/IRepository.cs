﻿using EPRN.Common.Data;
using EPRN.Common.Data.DataModels;


namespace EPRN.Waste.API.Repositories.Interfaces
{
    public interface IRepository
    {

        Task AddJourney(WasteJourney journey);

        Task UpdateJourneySiteId(int journeyId, int siteId);

        Task UpdateJourneyBaledWithWire(int journeyId, bool baledWithWire);

        Task UpdateJourneyTonnage(int journeyId, double journeyTonnage);

        Task UpdateJourneyDoneId(int journeyId, DoneWaste selectedDoneWaste);

        Task UpdateJourneyWasteTypeId(int journeyId, int wasteTypeId);

        Task UpdateJourneyMonth(int journeyId, int selectedMonth);

        Task UpdateJourneySubTypeAndAdjustment(
            int journeyId,
            int subTypeId,
            double adjustment);

        Task<IList<WasteType>> GetAllWasteTypes();

        Task<IList<WasteSubType>> GetWasteSubTypes(int wasteTypeId);

        Task<string> GetWasteTypeName(int journeyId);

        Task<int?> GetWasteTypeId(int journeyId);

        Task<WasteJourney> GetWasteSubTypeSelection(int journeyId);

        Task<DoneWaste?> GetDoneWaste(int journeyId);

        Task<WasteJourney> GetWasteJourneyById(int jounreyId);

        Task<double?> GetWasteTonnage(int journeyId);

        Task<bool> Exists(int journeyId);

        Task<string> GetWasteNote(int journeyId);
    }
}
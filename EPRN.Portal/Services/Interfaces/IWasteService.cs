﻿using EPRN.Portal.ViewModels.Waste;

namespace EPRN.Portal.Services.Interfaces
{
    public interface IWasteService
    {
        Task<int> CreateJourney();

        Task<DuringWhichMonthRequestViewModel> GetQuarterForCurrentMonth(int journeyId, int currentMonth);

        Task SaveSelectedMonth(DuringWhichMonthRequestViewModel duringWhichMonthRequestViewModel);

        Task<WasteTypesViewModel> GetWasteTypesViewModel(int journeyId);

        Task<WasteSubTypesViewModel> GetWasteSubTypesViewModel(int journeyId);

        Task SaveSelectedWasteType(WasteTypesViewModel wasteTypesViewModel);

        Task SaveSelectedWasteSubType(WasteSubTypesViewModel wasteSubTypesViewModel);

        Task<WhatHaveYouDoneWasteModel> GetWasteModel(int journeyId);

        Task<WasteRecordStatusViewModel> GetWasteRecordStatus(int journeyId);

        Task SaveWhatHaveYouDoneWaste(WhatHaveYouDoneWasteModel whatHaveYouDoneWasteModel);

        Task<BaledWithWireModel> GetBaledWithWireModel(int journeyId);

        Task<ExportTonnageViewModel> GetExportTonnageViewModel(int journeyId);

        Task SaveTonnage(ExportTonnageViewModel exportTonnageViewModel);
        Task SaveBaledWithWire(BaledWithWireModel baledWireModel);

        Task<ReProcessorExportViewModel> GetReProcessorExportViewModel(int journeyId);

        Task SaveReprocessorExport(ReProcessorExportViewModel reprocessorExportViewModel);

        Task<NoteViewModel> GetNoteViewModel(int journeyId);

        Task SaveNote(NoteViewModel noteViewModel);

    }
}

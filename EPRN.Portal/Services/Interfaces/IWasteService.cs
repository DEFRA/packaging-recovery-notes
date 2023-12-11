﻿using EPRN.Portal.ViewModels;

namespace EPRN.Portal.Services.Interfaces
{
    public interface IWasteService
    {
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

        ExportTonnageViewModel GetExportTonnageViewModel(int journeyId);

        Task SaveTonnage(ExportTonnageViewModel exportTonnageViewModel);
        Task SaveBaledWithWire(BaledWithWireModel baledWireModel);

        Task<ReProcessorExportViewModel> GetReProcessorExportViewModel(int journeyId);

        Task SaveReprocessorExport(ReProcessorExportViewModel reprocessorExportViewModel);
    }
}

﻿using EPRN.Common.Dtos;

namespace EPRN.Waste.API.Services.Interfaces
{
    public interface IWasteService
    {
        Task<IEnumerable<WasteTypeDto>> WasteTypes();

        Task<IEnumerable<WasteSubTypeDto>> WasteSubTypes(int wasteTypeid);
    }
}

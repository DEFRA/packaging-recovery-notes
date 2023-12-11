﻿using EPRN.Common.Dtos;
using EPRN.Common.Enums;
using EPRN.Portal.RESTServices.Interfaces;
using EPRN.Portal.Services;

namespace EPRN.Portal.RESTServices
{
    public class HttpWasteService : BaseHttpService, IHttpWasteService
    {
        private const string journeyRoutePart = "Journey";
        public HttpWasteService(
            string baseUrl,
            IHttpClientFactory httpClientFactory) : base(baseUrl, httpClientFactory)
        {
        }

        public async Task<IEnumerable<WasteTypeDto>> GetWasteMaterialTypes()
        {
            return await Get<List<WasteTypeDto>>("WasteTypes");
        }

        public async Task<WasteTypeDto> GetWasteMaterialType(int journeyId)
        {
            return await Get<WasteTypeDto>($"WasteTypes/{journeyId}");
        }

        public async Task<WasteRecordStatusDto> GetWasteRecordStatus(int journeyId)
        {
            return await Get<WasteRecordStatusDto>($"{journeyRoutePart}/{journeyId}/status");
        }

        public async Task SaveSelectedMonth(int journeyId, int selectedMonth)
        {
            await Post($"{journeyRoutePart}/{journeyId}/Month/{selectedMonth}");
        }

        public async Task SaveSelectedWasteType(int journeyId, int selectedWasteTypeId)
        {
            await Post($"{journeyRoutePart}/{journeyId}/Type/{selectedWasteTypeId}");
        }

        public async Task<DoneWaste> GetWhatHaveYouDoneWaste(int journeyId)
        {
            return await Get<DoneWaste>($"{journeyRoutePart}/{journeyId}/WhatHaveYouDoneWaste");
        }

        public async Task SaveWhatHaveYouDoneWaste(int journeyId, DoneWaste whatHaveYouDoneWaste)
        {
            await Post($"{journeyRoutePart}/{journeyId}/Done/{whatHaveYouDoneWaste}");
        }

        public async Task<string> GetWasteType(int journeyId)
        {
            return await Get<string>($"{journeyRoutePart}/{journeyId}/WasteType");
        }

        public async Task SaveTonnage(int journeyId, double tonnage)
        {
            await Post($"{journeyRoutePart}/{journeyId}/Tonnage/{tonnage}");
        }

        public async Task SaveBaledWithWire(int journeyId, bool bailedWithWire)
        {
            await Post($"Journey/{journeyId}/BaledWithWire/{bailedWithWire}");
        }

    }
}

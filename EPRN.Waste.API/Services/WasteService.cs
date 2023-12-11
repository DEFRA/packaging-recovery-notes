﻿using AutoMapper;
using EPRN.Common.Dtos;
using EPRN.Waste.API.Models;
using EPRN.Waste.API.Repositories.Interfaces;
using EPRN.Waste.API.Services.Interfaces;

namespace EPRN.Waste.API.Services
{
    public class WasteService : IWasteService
    {
        public readonly IMapper _mapper;
        public readonly IRepository _wasteRepository;

        public WasteService(
            IMapper mapper,
            IRepository wasteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _wasteRepository = wasteRepository ?? throw new ArgumentNullException(nameof(_wasteRepository));
        }

        public async Task<IEnumerable<WasteTypeDto>> WasteTypes(bool lazyLoad = false)
        {
            _wasteRepository.LazyLoading = lazyLoad;
            await Task.CompletedTask;
            // we want the entire table contents (at the
            // moment - there may be more requirements in the future)
            // so no where clause
            var dbWasteTypes = _wasteRepository
                .List<WasteType>()
                .OrderBy(wt => wt.Name)
                .ToList();

            return _mapper.Map<List<WasteTypeDto>>(dbWasteTypes);
        }

        public async Task<IEnumerable<WasteSubTypeDto>> WasteSubTypes(int wasteTypeId, bool lazyLoad = false)
        {
            var wasteTypes = await WasteTypes(true);
            var wasteType = wasteTypes.SingleOrDefault(x => x.Id == wasteTypeId);
            var wasteSubTypes = wasteType.SubTypes.Select(x => new WasteSubTypeDto { Adjustment = x.Adjustment, Id = x.Id, Name= x.Name });

            return wasteSubTypes;
        }
    }
}

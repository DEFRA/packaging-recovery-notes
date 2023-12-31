﻿namespace EPRN.Common.Dtos
{
    public class WasteSubTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Adjustment { get; set; }

        public bool AdjustmentPercentageRequired { get; set; }

        public int WasteTypeId { get; set; }

        public WasteTypeDto WasteType { get; set; }
    }
}

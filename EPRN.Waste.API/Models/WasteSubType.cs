﻿using System.ComponentModel.DataAnnotations;

namespace EPRN.Waste.API.Models
{
    public class WasteSubType : IdBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null;

        public double? Adjustment { get; set; } = null;

        public int WasteTypeId { get; set; }

        public bool AdjustmentPercentageRequired { get; set; }

        public WasteType WasteType { get; set; }

        public ICollection<WasteJourney> WasteJourneys { get; set; }
    }
}
﻿namespace EPRN.Common.Data.DataModels
{
    public abstract class BaseEntity : IdBaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? LastModifiedDate { get; set; }

        public string? LastModifiedBy { get; set; } = null;
    }
}

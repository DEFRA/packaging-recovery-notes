﻿using EPRN.Common.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace EPRN.Common.Data
{
    public class EPRNContext : DbContext
    {
        public EPRNContext()
        {
        }

        public EPRNContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        public virtual DbSet<WasteType> WasteType { get; set; }

        public virtual DbSet<WasteSubType> WasteSubType { get; set; }

        public virtual DbSet<WasteJourney> WasteJourney { get; set; }

        public virtual DbSet<PackagingRecoveryNote> PRN { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    public partial class SpillTrackerDbContext : DbContext
    {
        public SpillTrackerDbContext()
        {
        }

        public SpillTrackerDbContext(DbContextOptions<SpillTrackerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chemical> Chemicals { get; set; }
        public virtual DbSet<ChemicalState> ChemicalStates { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContactInfo> ContactInfoes { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FacilityChemical> FacilityChemicals { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<StatusTime> StatusTimes { get; set; }
        public virtual DbSet<Stuser> Stusers { get; set; }
        public virtual DbSet<StuserFacility> StuserFacilities { get; set; }
        public virtual DbSet<Surface> Surfaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=SpillTrackerConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("Facility_FK_CompanyID");
            });

            modelBuilder.Entity<FacilityChemical>(entity =>
            {
                entity.HasOne(d => d.Chemical)
                    .WithMany(p => p.FacilityChemicals)
                    .HasForeignKey(d => d.ChemicalId)
                    .HasConstraintName("FacilityChemicals_FK_ChemicalID");

                entity.HasOne(d => d.ChemicalState)
                    .WithMany(p => p.FacilityChemicals)
                    .HasForeignKey(d => d.ChemicalStateId)
                    .HasConstraintName("FacilityChemicals_FK_ChemicalStateID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.FacilityChemicals)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("FacilityChemicals_FK_FacilityID");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasOne(d => d.ChemicalState)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.ChemicalStateId)
                    .HasConstraintName("Form_FK_ChemicalStateID");

                entity.HasOne(d => d.FacilityChemical)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.FacilityChemicalId)
                    .HasConstraintName("Form_FK_FacilityChemicalID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("Form_FK_FacilityID");

                entity.HasOne(d => d.SpillSurface)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.SpillSurfaceId)
                    .HasConstraintName("Form_FK_Spill_SurfaceID");

                entity.HasOne(d => d.Stuser)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.StuserId)
                    .HasConstraintName("Form_FK_STUserID");
            });

            modelBuilder.Entity<Stuser>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Stusers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("STUser_FK_CompanyID");
            });

            modelBuilder.Entity<StuserFacility>(entity =>
            {
                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.StuserFacilities)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("StuserFacilities_FK_FacilityId");

                entity.HasOne(d => d.Stuser)
                    .WithMany(p => p.StuserFacilities)
                    .HasForeignKey(d => d.StuserId)
                    .HasConstraintName("StuserFacilities_FK_StuserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExpeditionProject.Models
{
    public partial class HimalayasDbContext : DbContext
    {
        public HimalayasDbContext()
        {
        }

        public HimalayasDbContext(DbContextOptions<HimalayasDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Climber> Climbers { get; set; }
        public virtual DbSet<Expedition> Expeditions { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Peak> Peaks { get; set; }
        public virtual DbSet<TrekkingAgency> TrekkingAgencies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionofDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("BlogPost_FK_User");
            });

            modelBuilder.Entity<Climber>(entity =>
            {
                entity.HasOne(d => d.Expedition)
                    .WithMany(p => p.Climbers)
                    .HasForeignKey(d => d.ExpeditionId)
                    .HasConstraintName("Member_FK_Expedition");
            });

            modelBuilder.Entity<Expedition>(entity =>
            {
                entity.HasOne(d => d.Peak)
                    .WithMany(p => p.Expeditions)
                    .HasForeignKey(d => d.PeakId)
                    .HasConstraintName("Expedition_FK_Peak");

                entity.HasOne(d => d.TrekkingAgency)
                    .WithMany(p => p.Expeditions)
                    .HasForeignKey(d => d.TrekkingAgencyId)
                    .HasConstraintName("Expedition_FK_TrekkingAgency");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasOne(d => d.Expedition)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.ExpeditionId)
                    .HasConstraintName("Form_FK_Expedition");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Form_Fk_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("User_FK_UserType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

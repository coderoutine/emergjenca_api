using System;
using Emergency.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Emergency.DAL.Data
{
    public partial class EContext : DbContext
    {
        public EContext()
        {
        }

        public EContext(DbContextOptions<EContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactPerson> ContactPerson { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Shelter> Shelter { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }

   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.ToTable("Contact Person");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Shelter)
                    .WithMany(p => p.ContactPerson)
                    .HasForeignKey(d => d.ShelterId)
                    .HasConstraintName("FK_Contact Person_Shelter");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Lat)
                    .IsRequired()
                    .HasMaxLength(25);
                entity.Property(e => e.Lng)
                 .IsRequired()
                 .HasMaxLength(25);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Shelter>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Shelter)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Shelter_Event");
            });

            modelBuilder.Entity<Supplies>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.ContactPerson)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.ContactPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplies_Contact Person");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Supplies_Event");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Volunteer_Event");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

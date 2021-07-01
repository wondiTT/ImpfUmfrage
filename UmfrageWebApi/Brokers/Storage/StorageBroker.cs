using System;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UmfrageWebApi.DbModels;

#nullable disable

namespace UmfrageWebApi.Brokers.Storage
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        public StorageBroker()
        {
        }

        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Personen { get; set; }
        public virtual DbSet<PersonArt> PersonArten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Ausweisnummer)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Geburtsdatum).HasColumnType("datetime");

                entity.Property(e => e.LetzteAenderung).HasColumnType("datetime");

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Strasse)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);

                entity.HasOne(d => d.PersonArt)
                    .WithMany(p => p.Personen)
                    .HasForeignKey(d => d.PersonArtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Person__PersonArt");
            });

            modelBuilder.Entity<PersonArt>(entity =>
            {
                entity.ToTable("PersonArt");

                entity.Property(e => e.PersonArtId).HasColumnName("PersonArtID");

                entity.Property(e => e.Beschreibung)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.LetzteAenderung).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

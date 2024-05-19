using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BibliothequeVieuxMontreal.Models
{
    public partial class biblioContext : DbContext
    {
        public biblioContext()
        {
        }

        public biblioContext(DbContextOptions<biblioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Livre> Livres { get; set; } = null!;
        public virtual DbSet<Membre> Membres { get; set; } = null!;
        public virtual DbSet<Pret> Prets { get; set; } = null!;
        public virtual DbSet<Retard> Retards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=biblio;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livre>(entity =>
            {
                entity.ToTable("Livre");

                entity.Property(e => e.Auteur)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Cetegorie)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Titre).HasMaxLength(100);
            });

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.ToTable("Membre");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pret>(entity =>
            {
                entity.ToTable("Pret");

                entity.Property(e => e.DateDebut)
                    .HasColumnType("date")
                    .HasColumnName("date_debut");

                entity.Property(e => e.DateFin)
                    .HasColumnType("date")
                    .HasColumnName("date_fin");

                entity.Property(e => e.IdLivre).HasColumnName("Id_livre");

                entity.Property(e => e.IdMembre).HasColumnName("Id_membre");

                entity.HasOne(d => d.IdLivreNavigation)
                    .WithMany(p => p.Prets)
                    .HasForeignKey(d => d.IdLivre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pret__Id_livre__5FB337D6");

                entity.HasOne(d => d.IdMembreNavigation)
                    .WithMany(p => p.Prets)
                    .HasForeignKey(d => d.IdMembre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pret__Id_membre__60A75C0F");
            });

            modelBuilder.Entity<Retard>(entity =>
            {
                entity.ToTable("Retard");

                entity.Property(e => e.DatePret).HasColumnType("date");

                entity.Property(e => e.IdLivre).HasColumnName("Id_livre");

                entity.Property(e => e.IdMembre).HasColumnName("Id_membre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

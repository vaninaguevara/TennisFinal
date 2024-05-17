﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tennis.Models.Entities;

namespace Tennis.Models
{
    public class Torneo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int? CreatedByUserId { get; set; }
        public string Genero { get; set; }
        public List<TorneoJugador> TorneoJugador { get; set; }
        public bool? Termino { get; set; }
        public DateTime? FechaTermino { get; set; }
    }
    public class TorneoConfig : IEntityTypeConfiguration<Torneo>
    {
        public void Configure(EntityTypeBuilder<Torneo> builder)
        {
            builder.ToTable("Torneo");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(t => t.Nombre).HasColumnName("Nombre").IsRequired().HasMaxLength(100);

            builder.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId").IsRequired();

            builder.Property(t => t.Genero).HasColumnName("Genero").HasMaxLength(10);

            builder.Property(t => t.FechaTermino).HasColumnName("FechaTermino");

            builder.Property(t => t.Termino).HasColumnName("Termino").HasDefaultValueSql("0");

            builder.HasMany(t => t.TorneoJugador)
                   .WithOne()
                   .HasForeignKey("IdTorneo")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Tennis.Models.Entities
{
    public class TorneoJugador
    {
        [Key]
        public int Id { get; set; }

        public int JugadorId { get; set; }
        [ForeignKey(nameof(JugadorId))]
        public virtual Jugador Jugador { get; set; }

        public int IdTorneo { get; set; }
        [ForeignKey(nameof(IdTorneo))]
        public virtual Torneo Torneo { get; set; }
    }
    public class TorneoJugadorConfig : IEntityTypeConfiguration<TorneoJugador>
    {
        public void Configure(EntityTypeBuilder<TorneoJugador> builder)
        {
            builder.ToTable("TorneoJugador");
            builder.HasKey(tj => tj.Id);

            builder.Property(tj => tj.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(tj => tj.JugadorId)
                .HasColumnName("JugadorId")
                .IsRequired();

            builder.Property(tj => tj.IdTorneo)
                .HasColumnName("IdTorneo")
                .IsRequired();

            builder.HasOne(tj => tj.Jugador)
                   .WithMany()
                   .HasForeignKey(tj => tj.JugadorId);

            builder.HasOne(tj => tj.Torneo)
                   .WithMany()
                   .HasForeignKey(tj => tj.IdTorneo);
        }
    }
}

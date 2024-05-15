using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tennis.Models
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }
        public int IdTorneo { get; set; }
        [ForeignKey(nameof(IdTorneo))]
        public virtual Torneo Torneo { get; set; }
        public int IdJugador1 { get; set; }
        [ForeignKey(nameof(IdJugador1))]
        public Jugador Jugador1 { get; set; }
        public int IdJugador2 { get; set; }
        [ForeignKey(nameof(IdJugador2))]
        public Jugador Jugador2 { get; set; }
        public string? Resultado { get; set; }
    }
    public class PartidoConfig : IEntityTypeConfiguration<Partido>
    {
        public void Configure(EntityTypeBuilder<Partido> builder)
        {
            builder.ToTable("Partido");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.IdTorneo).HasColumnName("IdTorneo").IsRequired();
            builder.Property(x => x.IdJugador1).HasColumnName("IdJugador1").IsRequired();
            builder.Property(x => x.IdJugador2).HasColumnName("IdJugador2").IsRequired();
            builder.Property(x => x.Resultado).HasColumnName("Resultado");

            builder.HasOne(p => p.Torneo)
                   .WithMany()
                   .HasForeignKey(p => p.IdTorneo)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Jugador1)
                   .WithMany() 
                   .HasForeignKey(p => p.IdJugador1)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Jugador2)
                   .WithMany() 
                   .HasForeignKey(p => p.IdJugador2)
                   .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}

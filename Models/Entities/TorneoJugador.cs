using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace Tennis.Models.Entities
{
    public class TorneoJugador
    {
        public int Id { get; set; }
       // [Column("JugadorId")]
        public int JugadorId { get; set; }
      //  [ForeignKey(nameof(JugadorId))]
        public virtual Jugador? Jugador { get; set; }

      //  [Column("TorneoId")]
        public int TorneoId { get; set; }
       // [ForeignKey(nameof(TorneoId))]

        public virtual Torneo? Torneo { get; set; }
    }
    public class TorneoJugadorConfig : IEntityTypeConfiguration<TorneoJugador>
    {
        public void Configure(EntityTypeBuilder<TorneoJugador> builder)
        {
            builder.ToTable("TorneoJugador");      

            builder.Property(tj => tj.JugadorId)
                .HasColumnName("JugadorId")
                .IsRequired();
            builder.Property(tj => tj.Id)
               .HasColumnName("Id")
               .IsRequired();

            builder.Property(tj => tj.TorneoId)
                .HasColumnName("TorneoId")
                .IsRequired();

            builder.HasOne(tj => tj.Jugador)
                   .WithMany(tj => tj.TorneoJugador)
                   .HasForeignKey(tj => tj.JugadorId)
                   .HasConstraintName("FK_Jugador")
                   .OnDelete(DeleteBehavior.SetNull);
          //  builder.HasKey(tj => new { tj.JugadorId, tj.TorneoId });
            builder.HasOne(tj => tj.Torneo)
                   .WithMany(tj => tj.TorneoJugador)
                   .HasForeignKey(tj => tj.TorneoId)
                   .HasConstraintName("FK_Torneo")
                   .OnDelete(DeleteBehavior.SetNull);
            
;
        }
    }
}

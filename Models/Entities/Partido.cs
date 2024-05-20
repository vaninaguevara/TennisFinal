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
        public int IdJugadorL { get; set; }
        [ForeignKey(nameof(IdJugadorL))]
        public Jugador JugadorL { get; set; }
        public int IdJugadorW { get; set; }
        [ForeignKey(nameof(IdJugadorW))]
        public Jugador JugadorW { get; set; }
    }
    public class PartidoConfig : IEntityTypeConfiguration<Partido>
    {
        public void Configure(EntityTypeBuilder<Partido> builder)
        {
            builder.ToTable("Partido");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.IdTorneo).HasColumnName("IdTorneo").IsRequired();
            builder.Property(x => x.IdJugadorL).HasColumnName("IdJugadorL").IsRequired();
            builder.Property(x => x.IdJugadorW).HasColumnName("IdJugadorW").IsRequired();

            builder.HasOne(p => p.Torneo)
                   .WithMany(p => p.Partido)
                   .HasForeignKey(p => p.IdTorneo)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.JugadorL).WithOne(i => i.Id)
                   .HasForeignKey(p => p.IdJugadorL)
                   .HasConstraintName("FK_IdJugador")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.JugadorW)
                   .WithMany() 
                   .HasForeignKey(p => p.IdJugadorW)
                   .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}

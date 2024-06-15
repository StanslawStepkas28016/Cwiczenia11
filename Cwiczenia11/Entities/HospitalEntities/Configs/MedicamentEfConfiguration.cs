using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cwiczenia11.Entities.HospitalEntities.Configs;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        // Ustalenie klucza głównego dla tabeli.
        builder
            .HasKey(x => x.IdMedicament)
            .HasName("Medicament_pk");

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .IsRequired();

        builder
            .Property(x => x.Type)
            .IsRequired();

        builder
            .ToTable("Medicament");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cwiczenia11.Entities.HospitalEntities.Configs;

public class DoctorEfConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // Ustawienie kolumny jako klucz główny.
        builder
            .HasKey(x => x.IdDoctor)
            .HasName("Doctor_pk");

        // Klucz nie jest auto generowany - nie jest IDENTITY.
        builder
            .Property(x => x.IdDoctor)
            .ValueGeneratedNever();

        // Pole FirstName, LastName i Email jest wymagane.
        builder
            .Property(x => x.FirstName)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .IsRequired();

        builder.ToTable("Doctor");
    }
}
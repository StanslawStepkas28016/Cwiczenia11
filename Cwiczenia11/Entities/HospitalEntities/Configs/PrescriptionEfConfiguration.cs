using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cwiczenia11.Entities.HospitalEntities.Configs;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        // Ustalenie klucza dla tabeli.
        builder
            .HasKey(x => x.IdPrescription)
            .HasName("Prescription_pk");

        // Klucz główny jest automatycznie generowany.
        builder
            .Property(x => x.IdPrescription)
            .UseIdentityColumn();

        // Ustalenie powiązania z Tabelą Doctor.
        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.IdDoctor)
            .HasConstraintName("Prescription_Doctor")
            .OnDelete(DeleteBehavior.Restrict);

        // Ustalenie powiązania z Tabelą Patient.
        builder
            .HasOne(x => x.Patient)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.IdPrescription)
            .HasConstraintName("Prescription_Patient")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Prescription");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cwiczenia11.Entities.HospitalEntities.Configs;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        // Ustalenie podwójnego klucza głównego.
        builder
            .HasKey(x => new { x.IdMedicament, x.IdPrescription })
            .HasName("PrescriptionMedicament_pk");

        builder
            .Property(x => x.Details)
            .IsRequired();

        // Ustalenie powiązania z Tabelą Medicament
        builder
            .HasOne(x => x.Medicament)
            .WithMany(x => x.PrescriptionMedicaments)
            .HasForeignKey(x => x.IdMedicament)
            .HasConstraintName("PrescriptionMedicament_Medicament")
            .OnDelete(DeleteBehavior.Restrict);

        // Ustalenie powiązania z Tabelą Prescription.
        builder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.PrescriptionMedicaments)
            .HasForeignKey(x => x.IdPrescription)
            .HasConstraintName("PrescriptionMedicament_Prescription")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("Prescription_Medicament");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cwiczenia11.Entities.HospitalEntities.Configs;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .HasKey(x => x.IdPatient)
            .HasName("Patient_pk");

        builder
            .Property(x => x.IdPatient)
            .ValueGeneratedNever();

        builder
            .Property(x => x.FirstName)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .IsRequired();

        builder
            .Property(x => x.Birthdate)
            .IsRequired();

        builder
            .ToTable("Patient");
    }
}
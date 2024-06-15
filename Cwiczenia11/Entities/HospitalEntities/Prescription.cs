namespace Cwiczenia11.Entities.HospitalEntities;

public class Prescription
{
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    // Muszą być ID, ale też muszą być obiekty tych klas,
    // do których są powiązania.
    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Patient Patient { get; set; }

    // Dodajemy collection, jeżeli chcemy zasygnalizować połączenie z inną tabelą,
    // od tabeli, w której jesteśmy, nie musi ono tu zawsze być!
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } =
        new List<PrescriptionMedicament>();
}
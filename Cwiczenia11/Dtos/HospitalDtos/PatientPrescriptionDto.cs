namespace Cwiczenia11.Dtos.HospitalDtos;

public class PatientPrescriptionDto
{
    public PatientDto PatientDto { get; set; }

    public ICollection<MedicamentDto> MedicamentsDto { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }
}
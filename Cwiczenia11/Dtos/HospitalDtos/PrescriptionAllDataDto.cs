namespace Cwiczenia11.Dtos.HospitalDtos;

public class PrescriptionAllDataDto
{
    public int IdPrescription { get; set; }

    public ICollection<MedicamentDto> MedicamentDtos { get; set; }

    public DoctorDto DoctorDto { get; set; }
}
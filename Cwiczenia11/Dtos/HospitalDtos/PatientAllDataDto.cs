namespace Cwiczenia11.Dtos.HospitalDtos;

public class PatientAllDataDto
{
    public PatientDto PatientDto { get; set; }

    public ICollection<PrescriptionAllDataDto> PrescriptionAllDataDtos { get; set; }
}
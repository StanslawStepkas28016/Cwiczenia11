namespace Cwiczenia11.ModelsDtos.HospitalDtos;

public class PatientAllDataDto
{
    public PatientDto PatientDto { get; set; }

    public ICollection<PrescriptionAllDataDto> PrescriptionAllDataDtos { get; set; }
}
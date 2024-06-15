namespace Cwiczenia11.ModelsDtos.HospitalDtos;

public class PatientDto
{
    public int IdPatient { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
}
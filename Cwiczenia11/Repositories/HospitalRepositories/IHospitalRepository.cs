using Cwiczenia11.ModelsDtos.HospitalDtos;

namespace Cwiczenia11.Repositories.HospitalRepositories;

public interface IHospitalRepository
{
    public Task<int> AddPatientWithPrescription(PatientPrescriptionDto patientPrescriptionDto,
        CancellationToken cancellationToken);

    public Task<PatientAllDataDto> GetPatientAllData(int idPatient, CancellationToken cancellationToken);
}
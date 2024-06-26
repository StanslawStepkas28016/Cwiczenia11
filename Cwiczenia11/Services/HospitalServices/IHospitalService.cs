using Cwiczenia11.Dtos.HospitalDtos;

namespace Cwiczenia11.Services.HospitalServices;

public interface IHospitalService
{
    public Task<int> AddPatientWithPrescription(PatientPrescriptionDto patientPrescriptionDto,
        CancellationToken cancellationToken);

    public Task<PatientAllDataDto> GetPatientAllData(int idPatient, CancellationToken cancellationToken);
}
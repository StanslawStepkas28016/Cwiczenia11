using Cwiczenia11.ModelsDtos.HospitalDtos;
using Cwiczenia11.Repositories;
using Cwiczenia11.Repositories.HospitalRepositories;

namespace Cwiczenia11.Services.HospitalServices;

public class HospitalService : IHospitalService
{
    private IHospitalRepository _hospitalRepository;

    public HospitalService(IHospitalRepository hospitalRepository)
    {
        _hospitalRepository = hospitalRepository;
    }

    public async Task<int> AddPatientWithPrescription(PatientPrescriptionDto patientPrescriptionDto,
        CancellationToken cancellationToken)
    {
        var res = await _hospitalRepository.AddPatientWithPrescription(patientPrescriptionDto, cancellationToken);

        return res;
    }

    public async Task<PatientAllDataDto> GetPatientAllData(int idPatient, CancellationToken cancellationToken)
    {
        var res = await _hospitalRepository.GetPatientAllData(idPatient, cancellationToken);
        
        return res;
    }
}
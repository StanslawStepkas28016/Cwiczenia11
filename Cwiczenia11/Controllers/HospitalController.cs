using Cwiczenia11.Dtos.HospitalDtos;
using Cwiczenia11.Services.HospitalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HospitalController : ControllerBase
{
    private IHospitalService _hospitalService;

    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    /// <summary>
    ///     Endpoint used for adding a patient (if the patient does not exist, it is being created withing the endpoint logic)
    ///     and its associated prescription data. 
    /// </summary>
    /// <param name="patientPrescriptionDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("addPatientWithPrescription")]
    public async Task<IActionResult> AddPatientWithPrescription(PatientPrescriptionDto patientPrescriptionDto,
        CancellationToken cancellationToken)
    {
        await _hospitalService.AddPatientWithPrescription(patientPrescriptionDto, cancellationToken);
        return Ok("Prescription successfully added");
    }

    /// <summary>
    ///     Endpoint used for retrieving every data in the context, that is associated with the patient.
    /// </summary>
    /// <param name="idPatient"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("getPatientAllData/{idPatient:int}")]
    public async Task<IActionResult> GetPatientAllData(int idPatient, CancellationToken cancellationToken)
    {
        var res = await _hospitalService.GetPatientAllData(idPatient, cancellationToken);
        return Ok(res);
    }
}
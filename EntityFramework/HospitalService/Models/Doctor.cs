using System.ComponentModel.DataAnnotations;

namespace HospitalService.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string WorkDomain { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace HospitalService.Models;

public class Client
{
    public int ClientId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
}

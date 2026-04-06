namespace HospitalService.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int ClientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Time { get; set; }
    public Client Client { get; set; }
    public Doctor Doctor { get; set; }
}

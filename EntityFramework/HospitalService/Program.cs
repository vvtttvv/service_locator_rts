using HospitalService.Data;
using HospitalService.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalService;

public class Program
{
    public static void Main()
    {
        var context = new HospitalContext();
        
        context.Clients
            .ExecuteDelete();
        context.Doctors
            .ExecuteDelete();

        
        // Add
        context.Clients.Add(new Client{ Name = "Peter"});
        context.Clients.Add(new Client{ Name = "David"});
        context.Doctors.Add(new Doctor{ Name = "Alex", WorkDomain = "Stomatology"});
        context.Doctors.Add(new Doctor{ Name = "Dima", WorkDomain = "LOR"});
        context.SaveChanges();

        var client1 = context.Clients.Where(c => c.Name=="David").ToList()[0];
        var doctor1 = context.Doctors.Where(d => d.Name =="Dima").ToList()[0];
        
        
        var appointment1 = new Appointment()
        {
            ClientId = client1.ClientId,
            DoctorId = doctor1.DoctorId,
            Time = DateTime.UtcNow,
        };

        context.Appointments.Add(appointment1);
        context.SaveChanges();
        
        // Update
        context.Appointments
            .Where(a => a.DoctorId == doctor1.DoctorId)
            .ExecuteUpdate(a =>
                a.SetProperty(a => a.Time, new DateTime(2026, 4, 16,
                    9, 0, 0, DateTimeKind.Utc)));
        
        // Delete
        context.Appointments
            .Where(a => a.DoctorId == doctor1.DoctorId)
            .ExecuteDelete();
        

        var appointments = context.Appointments.ToList();
        foreach (var appointment in appointments)
        {
            Console.WriteLine($"Client {appointment.ClientId}, had an appointment with doctor {appointment.DoctorId}");
        }


    }
}
using HospitalService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalService.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");

        builder.HasKey(x => x.DoctorId);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.WorkDomain)
            .IsRequired();
    }
}
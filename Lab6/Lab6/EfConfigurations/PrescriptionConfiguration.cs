using Lab6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab6.EfConfigurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(pr => pr.IdPrescription);
        builder.Property(pr => pr.IdPrescription).ValueGeneratedOnAdd();
        builder.Property(pr => pr.Date).IsRequired().HasMaxLength(100);
        builder.Property(pr => pr.DueDate).IsRequired().HasMaxLength(100);
        builder.HasOne<Doctor>(pr => pr.IDoctorNavigation)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(pr => pr.IdDoctor)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Patient>(pr => pr.IdPatientNavigation)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(pr => pr.IdPatient)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
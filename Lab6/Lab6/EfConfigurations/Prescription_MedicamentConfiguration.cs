using Lab6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab6.EfConfigurations;

public class Prescription_MedicamentConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
{
    public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
    {
        builder.HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        
        builder.HasOne<Medicament>(pm => pm.IdMedicamentNavigation)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Prescription>(pm => pm.IdPrescriptionNavigation)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
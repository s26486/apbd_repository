using Lab6.EfConfigurations;
using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Context;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext()
    {
        
    }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(pb =>
        {
            pb.HasKey(p => p.IdPatient);
            pb.Property(p => p.IdPatient).ValueGeneratedOnAdd();
            pb.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            pb.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            pb.Property(p => p.BirthDate).IsRequired(false).HasMaxLength(100);
        });

        modelBuilder.Entity<Doctor>(d =>
        {
            d.HasKey(d => d.IdDoctor);
            d.Property(d => d.IdDoctor).ValueGeneratedOnAdd();
            d.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            d.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            d.Property(d => d.Email).IsRequired().HasMaxLength(100);

           
        });

        modelBuilder.Entity<Medicament>(m =>
        {
            m.HasKey(m => m.IdMedicament);
            m.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
            m.Property(m => m.Name).IsRequired().HasMaxLength(100);
            m.Property(m => m.Description).IsRequired(false).HasMaxLength(100);
            m.Property(m => m.Type).IsRequired().HasMaxLength(100);
            
        });

        modelBuilder.Entity<Prescription>(pr =>
        {
            pr.HasKey(pr => pr.IdPrescription);
            pr.Property(pr => pr.IdPrescription).ValueGeneratedOnAdd();
            pr.Property(pr => pr.Date).IsRequired().HasMaxLength(100);
            pr.Property(pr => pr.DueDate).IsRequired().HasMaxLength(100);
            pr.Property(pr => pr.IdPatient).IsRequired();
            pr.Property(pr => pr.IdDoctor).IsRequired();
        });

        modelBuilder.Entity<Prescription_Medicament>(pm =>
        {
            pm.HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
            pm.Property(pm => pm.Dose).IsRequired(false);
            pm.Property(pm => pm.Details).IsRequired(false).HasMaxLength(100);
        });
        
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());
        modelBuilder.ApplyConfiguration(new Prescription_MedicamentConfiguration());
    }
    
}
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;


public class MedicalSystemContext : DbContext
{
    public MedicalSystemContext(DbContextOptions<MedicalSystemContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Checkup> Checkups { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Patient → MedicalRecords (1-N)
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.MedicalRecords)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Patient → Checkups (1-N)
        modelBuilder.Entity<Checkup>()
            .HasOne(c => c.Patient)
            .WithMany(p => p.Checkups)
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Checkup → Images (1-N)
        modelBuilder.Entity<Image>()
            .HasOne(i => i.Checkup)
            .WithMany(c => c.Images)
            .HasForeignKey(i => i.CheckupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Patient → Prescriptions (1-N)
        modelBuilder.Entity<Prescription>()
            .HasOne(pr => pr.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(pr => pr.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

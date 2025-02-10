using System.ComponentModel.DataAnnotations;

namespace ProjectV2.Models.Entities
{


    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Sex { get; set; }

        // Relations
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<Checkup> Checkups { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
    }




}



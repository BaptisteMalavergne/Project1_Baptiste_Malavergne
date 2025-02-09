using ProjectV2.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectV2.Models
{


    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; } // Ajouté
        public string Sex { get; set; } // Ajouté

        // Relations
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<Checkup> Checkups { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
    }




}



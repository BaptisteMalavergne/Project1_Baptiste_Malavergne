using ProjectV2.Models.Entities;

namespace ProjectV2.Models.DTO
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }

        // Si tu veux renvoyer les dossiers médicaux, tu peux les inclure avec une liste réduite d'attributs
        public List<MedicalRecordDTO> MedicalRecords { get; set; }
        public List<Checkup> Checkups { get; set; } 
        public List<Prescription> Prescriptions { get; set; } 
    }

}

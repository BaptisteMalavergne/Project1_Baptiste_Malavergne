using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectV2.Models
{

    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        [Required]
        public int PatientId { get; set; }  // Remarque ici : C'est juste un ID, pas un objet complet.

        public Patient Patient { get; set; }  // La relation avec Patient (facultatif pour la création).
    }





}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectV2.Models.Entities
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Instructions { get; set; }

        // Relation with Checkup
        [Required]
        public int CheckupId { get; set; }

        [ForeignKey("CheckupId")]
        public Checkup Checkup { get; set; }
    }
}
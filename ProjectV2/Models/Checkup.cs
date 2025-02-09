
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectV2.Models
{

    public class Checkup
    {
        public int CheckupId { get; set; }
        public DateTime CheckupDate { get; set; }
        public string ProcedureCode { get; set; }

        // Relation avec Patient
        public int PatientId { get; set; }
        public Patient Patient { get; set; }  // Navigation property vers Patient
        public ICollection<Image> Images { get; set; }
    }






}

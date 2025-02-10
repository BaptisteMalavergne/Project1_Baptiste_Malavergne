namespace ProjectV2.Models.DTO
{
    public class PrescriptionCreateDTO
    {
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public int CheckupId { get; set; }
    }
}
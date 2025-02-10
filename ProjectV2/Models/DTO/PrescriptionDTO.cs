namespace ProjectV2.Models.DTO
{
    public class PrescriptionDTO
    {
        public int PrescriptionId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public int CheckupId { get; set; }
    }
}
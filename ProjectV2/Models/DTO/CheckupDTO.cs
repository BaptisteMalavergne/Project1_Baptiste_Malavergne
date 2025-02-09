namespace ProjectV2.Models.DTO
{
    public class CheckupDTO
    {
        public int CheckupId { get; set; }
        public DateTime CheckupDate { get; set; }
        public string ProcedureCode { get; set; }
        public int PatientId { get; set; }

        // Optionnel : Ajouter les images liées au checkup si tu veux les afficher.
        public ICollection<ImageDTO> Images { get; set; }  // Si tu as une classe ImageDTO
    }
}

// CheckupDTO.cs
namespace ProjectV2.Models.DTO
{
    public class CheckupDTO
    {
        public int CheckupId { get; set; }
        public DateTime CheckupDate { get; set; }
        public string ProcedureCode { get; set; }
        public int PatientId { get; set; }
        public string PatientFullName { get; set; } // Pour afficher le nom complet du patient
        public ICollection<string> ImageUrls { get; set; } // Liste des URLs des images associées

        public CheckupDTO()
        {
            ImageUrls = new List<string>(); // Initialiser la collection d'images
        }
    }
}


namespace ProjectV2.Models.DTO
{
    public class ImageDTO
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int CheckupId { get; set; }  // Le CheckupId pour montrer à quel checkup l'image est liée
    }
}

namespace ProjectV2.Models.DTO
{
    public class ImageUploadDto
    {
        public IFormFile ImageFile { get; set; }
        public int CheckupId { get; set; }
    }
}

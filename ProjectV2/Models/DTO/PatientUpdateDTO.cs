using System.ComponentModel.DataAnnotations;

namespace ProjectV2.Models.DTO
{
    public class PatientUpdateDTO
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Sex { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectV2.Models
{

    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // Relation avec Checkup
        [ForeignKey("Checkup")]
        public int CheckupId { get; set; }
        public Checkup Checkup { get; set; }
    }



}

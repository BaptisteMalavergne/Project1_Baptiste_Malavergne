﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectV2.Models.Entities
{
    public class Checkup
    {
        [Key]
        public int CheckupId { get; set; }

        [Required]
        public DateTime CheckupDate { get; set; }

        [Required]
        public string ProcedureCode { get; set; }

        // Relation avec Patient
        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }  // Navigation property vers Patient

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Pages.Patients
{
    public class DetailsModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public DetailsModel(MedicalSystemContext context)
        {
            _context = context;
        }

        public PatientDTO Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.MedicalRecords) // Include related medical records
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            Patient = new PatientDTO
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Sex = patient.Sex,
                MedicalRecords = patient.MedicalRecords?.Select(mr => new MedicalRecordDTO
                {
                    MedicalRecordId = mr.MedicalRecordId,
                    DiseaseName = mr.DiseaseName,
                    PatientId = mr.PatientId
                }).ToList()
            };

            return Page();
        }
    }
}

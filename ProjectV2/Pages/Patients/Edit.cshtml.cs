using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public EditModel(MedicalSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientDTO Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var patient = await _context.Patients
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
                Sex = patient.Sex
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == Patient.PatientId);

            if (patient == null)
            {
                return NotFound();
            }

            // Update only personal information, no checkups or prescriptions
            patient.FirstName = Patient.FirstName;
            patient.LastName = Patient.LastName;
            patient.DateOfBirth = Patient.DateOfBirth;
            patient.Sex = Patient.Sex;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Patients/Index"); // Redirect to the patient list page after update
        }
    }
}

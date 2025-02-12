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
        public PatientUpdateDTO Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            Patient = new PatientUpdateDTO
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

            var patient = await _context.Patients.FindAsync(Patient.PatientId);

            if (patient == null)
            {
                return NotFound();
            }

            patient.FirstName = Patient.FirstName;
            patient.LastName = Patient.LastName;
            patient.DateOfBirth = Patient.DateOfBirth;
            patient.Sex = Patient.Sex;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patients.Any(e => e.PatientId == Patient.PatientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
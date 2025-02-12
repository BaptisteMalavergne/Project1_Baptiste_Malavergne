using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public DeleteModel(MedicalSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientDTO Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public CreateModel(MedicalSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientCreateDTO Patient { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var patient = new Patient
            {
                FirstName = Patient.FirstName,
                LastName = Patient.LastName,
                DateOfBirth = Patient.DateOfBirth,
                Sex = Patient.Sex
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
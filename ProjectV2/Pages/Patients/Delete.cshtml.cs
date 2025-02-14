using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.Entities;
using System.Threading.Tasks;

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
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the patient by ID
            Patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (Patient == null)
            {
                return NotFound();  // Patient not found, return a 404 page
            }

            return Page();
        }
    }


}

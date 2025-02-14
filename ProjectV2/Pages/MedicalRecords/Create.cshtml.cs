using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using System.Threading.Tasks;

namespace ProjectV2.Pages.MedicalRecords
{
    public class CreateModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        [BindProperty]
        public MedicalRecordCreateDTO MedicalRecord { get; set; }

        public CreateModel(MedicalSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var newRecord = new MedicalRecord
            {
                DiseaseName = MedicalRecord.DiseaseName,
                PatientId = MedicalRecord.PatientId
            };

            _context.MedicalRecords.Add(newRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

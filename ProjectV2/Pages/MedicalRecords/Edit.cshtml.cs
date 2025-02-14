using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectV2.Pages.MedicalRecords
{
    public class EditModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        [BindProperty]
        public MedicalRecordDTO MedicalRecord { get; set; }

        public EditModel(MedicalSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return NotFound();

            MedicalRecord = new MedicalRecordDTO
            {
                MedicalRecordId = record.MedicalRecordId,
                DiseaseName = record.DiseaseName,
                PatientId = record.PatientId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var record = await _context.MedicalRecords.FindAsync(MedicalRecord.MedicalRecordId);
            if (record == null) return NotFound();

            record.DiseaseName = MedicalRecord.DiseaseName;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

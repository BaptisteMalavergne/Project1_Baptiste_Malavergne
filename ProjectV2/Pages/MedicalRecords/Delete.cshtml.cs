using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectV2.Models.Entities;
using System.Threading.Tasks;

namespace ProjectV2.Pages.MedicalRecords
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public DeleteModel(MedicalSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return NotFound();

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

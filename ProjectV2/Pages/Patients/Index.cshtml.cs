using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;

namespace ProjectV2.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public IndexModel(MedicalSystemContext context)
        {
            _context = context;
        }

        public IList<PatientDTO> Patients { get; set; }

        public async Task OnGetAsync()
        {
            Patients = await _context.Patients.Select(p => new PatientDTO
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Sex = p.Sex
            }).ToListAsync();
        }
    }
}
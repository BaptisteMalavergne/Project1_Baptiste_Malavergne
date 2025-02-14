using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectV2.Pages.MedicalRecords
{
    public class IndexModel : PageModel
    {
        private readonly MedicalSystemContext _context;

        public IndexModel(MedicalSystemContext context)
        {
            _context = context;
        }

        public List<MedicalRecordDTO> MedicalRecords { get; set; }

        public async Task OnGetAsync()
        {
            MedicalRecords = await _context.MedicalRecords
                .Select(r => new MedicalRecordDTO
                {
                    MedicalRecordId = r.MedicalRecordId,
                    DiseaseName = r.DiseaseName,
                    PatientId = r.PatientId
                }).ToListAsync();
        }
    }
}

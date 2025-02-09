/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;
using ProjectV2.Models.DTO;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckupsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public CheckupsController(MedicalSystemContext context)
        {
            _context = context;
        }

        // POST: api/Checkups
        [HttpPost]
        public async Task<ActionResult<CheckupDTO>> CreateCheckup(CheckupCreateDTO checkupDTO)
        {
            // Vérifie si le patient existe
            var patient = await _context.Patients.FindAsync(checkupDTO.PatientId);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            // Crée un nouveau checkup
            var checkup = new Checkup
            {
                CheckupDate = checkupDTO.CheckupDate,
                ProcedureCode = checkupDTO.ProcedureCode,
                PatientId = checkupDTO.PatientId
            };

            // Ajoute le checkup à la base de données
            _context.Checkups.Add(checkup);
            await _context.SaveChangesAsync();

            // Retourne le CheckupDTO en réponse
            var checkupDTOResponse = new CheckupDTO
            {
                CheckupId = checkup.CheckupId,
                CheckupDate = checkup.CheckupDate,
                ProcedureCode = checkup.ProcedureCode,
                PatientId = checkup.PatientId,
                // Si tu as des images, tu peux les ajouter ici aussi.
                Images = checkup.Images?.Select(i => new ImageDTO { //remplir les propriétés de ImageDTO }).ToList()
            };

            return CreatedAtAction(nameof(GetCheckup), new { id = checkup.CheckupId }, checkupDTOResponse);
        }

        // GET: api/Checkups/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckupDTO>> GetCheckup(int id)
        {
            var checkup = await _context.Checkups
                .Where(c => c.CheckupId == id)
                .Select(c => new CheckupDTO
                {
                    CheckupId = c.CheckupId,
                    CheckupDate = c.CheckupDate,
                    ProcedureCode = c.ProcedureCode,
                    PatientId = c.PatientId,
                    // Si tu veux afficher les images
                    Images = c.Images?.Select(i => new ImageDTO { // propriétés des images }).ToList()
                })
                .FirstOrDefaultAsync();

            if (checkup == null)
            {
                return NotFound();
            }

            return checkup;
        }

        // GET: api/Checkups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckupDTO>>> GetCheckups()
        {
            var checkups = await _context.Checkups
                .Select(c => new CheckupDTO
                {
                    CheckupId = c.CheckupId,
                    CheckupDate = c.CheckupDate,
                    ProcedureCode = c.ProcedureCode,
                    PatientId = c.PatientId,
                    // Si tu veux afficher les images
                    Images = c.Images?.Select(i => new ImageDTO { // propriétés des images  }).ToList()
                })
                .ToListAsync();

            return Ok(checkups);
        }

        // PUT: api/Checkups/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckup(int id, CheckupCreateDTO checkupDTO)
        {
            if (id != checkupDTO.PatientId)
            {
                return BadRequest();
            }

            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }

            checkup.CheckupDate = checkupDTO.CheckupDate;
            checkup.ProcedureCode = checkupDTO.ProcedureCode;

            _context.Entry(checkup).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Checkups/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckup(int id)
        {
            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }

            _context.Checkups.Remove(checkup);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}*/

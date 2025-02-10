// CheckupController.cs
using Microsoft.AspNetCore.Mvc;
using ProjectV2.Models;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckupController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public CheckupController(MedicalSystemContext context)
        {
            _context = context;
        }

        
        // GET: api/Checkup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckupDTO>>> GetCheckups()
        {
            var checkups = await _context.Checkups
                .Include(c => c.Patient)
                .Include(c => c.Images)
                .ToListAsync();

            var checkupDTOs = checkups.Select(c => new CheckupDTO
            {
                CheckupId = c.CheckupId,
                CheckupDate = c.CheckupDate,
                ProcedureCode = c.ProcedureCode,
                PatientId = c.PatientId,
                PatientFullName = $"{c.Patient.FirstName} {c.Patient.LastName}",
                ImageUrls = c.Images.Select(i => i.ImageUrl).ToList() // Assuming your Image model has a 'Url' property
            }).ToList();

            return Ok(checkupDTOs);
        }
        // POST: api/Checkup
        [HttpPost]
        public async Task<IActionResult> CreateCheckup([FromBody] CheckupCreateDTO checkupCreateDTO)
        {
            if (checkupCreateDTO == null)
            {
                return BadRequest();
            }

            var checkup = new Checkup
            {
                CheckupDate = checkupCreateDTO.CheckupDate,
                ProcedureCode = checkupCreateDTO.ProcedureCode,
                PatientId = checkupCreateDTO.PatientId
            };

            _context.Checkups.Add(checkup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckup", new { id = checkup.CheckupId }, checkup);
        }


        // GET: api/Checkup/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckupDTO>> GetCheckup(int id)
        {
            var checkup = await _context.Checkups
                .Include(c => c.Patient)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.CheckupId == id);

            if (checkup == null)
            {
                return NotFound();
            }

            var checkupDTO = new CheckupDTO
            {
                CheckupId = checkup.CheckupId,
                CheckupDate = checkup.CheckupDate,
                ProcedureCode = checkup.ProcedureCode,
                PatientId = checkup.PatientId,
                PatientFullName = $"{checkup.Patient.FirstName} {checkup.Patient.LastName}",
                ImageUrls = checkup.Images.Select(i => i.ImageUrl).ToList()
            };

            return Ok(checkupDTO);
        }

        // PUT: api/Checkup/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckup(int id, [FromBody] CheckupCreateDTO checkupCreateDTO)
        {
            if (id != checkupCreateDTO.PatientId) // Change this condition based on your use case
            {
                return BadRequest();
            }

            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }

            checkup.CheckupDate = checkupCreateDTO.CheckupDate;
            checkup.ProcedureCode = checkupCreateDTO.ProcedureCode;

            _context.Entry(checkup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Checkup/{id}
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

        private bool CheckupExists(int id)
        {
            return _context.Checkups.Any(e => e.CheckupId == id);
        }
    }
}

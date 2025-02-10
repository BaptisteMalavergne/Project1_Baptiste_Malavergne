using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using AutoMapper;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckupController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        private readonly IMapper _mapper;

        public CheckupController(MedicalSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Checkup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckupDTO>>> GetCheckups()
        {
            var checkups = await _context.Checkups
                .Include(c => c.Patient)
                .Include(c => c.Images)
                .ToListAsync();

            var checkupDTOs = _mapper.Map<IEnumerable<CheckupDTO>>(checkups);

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

            var checkup = _mapper.Map<Checkup>(checkupCreateDTO);

            _context.Checkups.Add(checkup);
            await _context.SaveChangesAsync();

            var checkupDTO = _mapper.Map<CheckupDTO>(checkup);

            return CreatedAtAction(nameof(GetCheckup), new { id = checkup.CheckupId }, checkupDTO);
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

            var checkupDTO = _mapper.Map<CheckupDTO>(checkup);

            return Ok(checkupDTO);
        }

        // PUT: api/Checkup/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckup(int id, [FromBody] CheckupCreateDTO checkupCreateDTO)
        {
            if (checkupCreateDTO == null)
            {
                return BadRequest();
            }

            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }

            // Ensure the CheckupId in the URL matches the CheckupId in the request body
            if (id != checkup.CheckupId)
            {
                return BadRequest("CheckupId in the URL and body do not match.");
            }

            // Update the properties
            checkup.CheckupDate = checkupCreateDTO.CheckupDate;
            checkup.ProcedureCode = checkupCreateDTO.ProcedureCode;
            checkup.PatientId = checkupCreateDTO.PatientId;

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

            var checkupDTO = _mapper.Map<CheckupDTO>(checkup);
            return Ok(checkupDTO);
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
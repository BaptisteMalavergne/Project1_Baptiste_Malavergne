using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using AutoMapper;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        private readonly IMapper _mapper;

        public PrescriptionsController(MedicalSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetPrescriptions()
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.Checkup)
                .ToListAsync();

            var prescriptionDTOs = _mapper.Map<IEnumerable<PrescriptionDTO>>(prescriptions);

            return Ok(prescriptionDTOs);
        }

        // GET: api/Prescriptions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> GetPrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.Checkup)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var prescriptionDTO = _mapper.Map<PrescriptionDTO>(prescription);

            return Ok(prescriptionDTO);
        }

        // POST: api/Prescriptions
        [HttpPost]
        public async Task<ActionResult<PrescriptionDTO>> CreatePrescription([FromBody] PrescriptionCreateDTO prescriptionCreateDTO)
        {
            if (prescriptionCreateDTO == null)
            {
                return BadRequest();
            }

            var prescription = _mapper.Map<Prescription>(prescriptionCreateDTO);

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            var prescriptionDTO = _mapper.Map<PrescriptionDTO>(prescription);

            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.PrescriptionId }, prescriptionDTO);
        }

        // PUT: api/Prescriptions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionCreateDTO prescriptionCreateDTO)
        {
            if (prescriptionCreateDTO == null)
            {
                return BadRequest();
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _mapper.Map(prescriptionCreateDTO, prescription);

            _context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var prescriptionDTO = _mapper.Map<PrescriptionDTO>(prescription);
            return Ok(prescriptionDTO);
        }

        // DELETE: api/Prescriptions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.PrescriptionId == id);
        }
    }
}
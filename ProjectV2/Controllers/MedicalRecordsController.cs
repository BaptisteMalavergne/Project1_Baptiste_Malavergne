using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;
using AutoMapper;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        private readonly IMapper _mapper;

        public MedicalRecordsController(MedicalSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordDTO>>> GetMedicalRecords()
        {
            var medicalRecords = await _context.MedicalRecords
                .Include(m => m.Patient)
                .ToListAsync();

            var medicalRecordDtos = _mapper.Map<IEnumerable<MedicalRecordDTO>>(medicalRecords);
            return Ok(medicalRecordDtos);
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDTO>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            var medicalRecordDto = _mapper.Map<MedicalRecordDTO>(medicalRecord);
            return Ok(medicalRecordDto);
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> CreateMedicalRecord(MedicalRecordCreateDTO medicalRecordCreateDto)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == medicalRecordCreateDto.PatientId);

            if (patient == null)
            {
                return NotFound($"Patient with ID {medicalRecordCreateDto.PatientId} not found.");
            }

            var medicalRecord = new MedicalRecord
            {
                DiseaseName = medicalRecordCreateDto.DiseaseName,
                PatientId = medicalRecordCreateDto.PatientId
            };

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            var medicalRecordDto = _mapper.Map<MedicalRecordDTO>(medicalRecord);
            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.MedicalRecordId }, medicalRecordDto);
        }

        // PUT: api/MedicalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, MedicalRecordCreateDTO medicalRecordUpdateDto)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            if (medicalRecord.PatientId != medicalRecordUpdateDto.PatientId)
            {
                return BadRequest("PatientId in the URL and body do not match.");
            }

            medicalRecord.DiseaseName = medicalRecordUpdateDto.DiseaseName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var medicalRecordDto = _mapper.Map<MedicalRecordDTO>(medicalRecord);
            return Ok(medicalRecordDto);
        }

        // DELETE: api/MedicalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(int id)
        {
            return _context.MedicalRecords.Any(e => e.MedicalRecordId == id);
        }
    }
}
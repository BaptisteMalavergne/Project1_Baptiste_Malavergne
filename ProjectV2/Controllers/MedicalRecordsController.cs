using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;
using ProjectV2.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public MedicalRecordsController(MedicalSystemContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient) // Inclure le patient dans la réponse
                .ToListAsync();
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }


        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord(MedicalRecordCreateDTO medicalRecordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Trouver le patient par son ID
            var patient = await _context.Patients.FindAsync(medicalRecordDto.PatientId);
            if (patient == null)
            {
                return NotFound();  // Si le patient n'existe pas, retourne une erreur
            }

            // Créer une instance de MedicalRecord en utilisant les données du DTO
            var medicalRecord = new MedicalRecord
            {
                DiseaseName = medicalRecordDto.DiseaseName,
                PatientId = medicalRecordDto.PatientId
                // Le medicalRecordId sera généré automatiquement par la base de données
            };

            // Ajouter le dossier médical dans la base de données
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.MedicalRecordId }, medicalRecord);
        }

        // PUT: api/MedicalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.MedicalRecordId)
            {
                return BadRequest();
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MedicalRecords.Any(e => e.MedicalRecordId == id))
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
    }
}
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;
using ProjectV2.Models.DTO;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public MedicalRecordsController(MedicalSystemContext context)
        {
            _context = context;
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> CreateMedicalRecord(MedicalRecordCreateDTO medicalRecordCreateDto)
        {
            // Créer une instance de MedicalRecord à partir du DTO
            var medicalRecord = new MedicalRecord
            {
                DiseaseName = medicalRecordCreateDto.DiseaseName,
                PatientId = medicalRecordCreateDto.PatientId
            };

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            // Retourner le DTO de MedicalRecord
            var medicalRecordDto = new MedicalRecordDTO
            {
                MedicalRecordId = medicalRecord.MedicalRecordId,
                DiseaseName = medicalRecord.DiseaseName,
                PatientId = medicalRecord.PatientId
            };

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.MedicalRecordId }, medicalRecordDto);
        }

        // Tu peux aussi ajouter un GET pour un MedicalRecord spécifique
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDTO>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords
                .FirstOrDefaultAsync(m => m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            var medicalRecordDto = new MedicalRecordDTO
            {
                MedicalRecordId = medicalRecord.MedicalRecordId,
                DiseaseName = medicalRecord.DiseaseName,
                PatientId = medicalRecord.PatientId
            };

            return Ok(medicalRecordDto);
        }
    }
}

*/
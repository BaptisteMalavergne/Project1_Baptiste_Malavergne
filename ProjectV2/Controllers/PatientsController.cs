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
    public class PatientsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public PatientsController(MedicalSystemContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients
                .Include(p => p.MedicalRecords)
                .Include(p => p.Checkups)
                .Include(p => p.Prescriptions)
                .ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(PatientCreateDTO patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth,
                Sex = patientDto.Sex
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId) // Correction ici
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patients.Any(e => e.PatientId == id)) // Correction ici
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

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}



/*
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV2.Models;
using ProjectV2.Models.DTO;

namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        private readonly IMapper _mapper;

        public PatientsController(MedicalSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientReadDTO>>> GetPatients()
        {
            var patients = await _context.Patients
                .Include(p => p.MedicalRecords) // Inclure les dossiers médicaux si nécessaire
                .ToListAsync();

            // Mappe les entités Patient vers les DTOs PatientReadDTO
            var patientDtos = _mapper.Map<IEnumerable<PatientReadDTO>>(patients);

            return Ok(patientDtos);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReadDTO>> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = _mapper.Map<PatientReadDTO>(patient);

            return Ok(patientDto);
        }

        // POST, PUT, DELETE... restent inchangées
    }
}
*/
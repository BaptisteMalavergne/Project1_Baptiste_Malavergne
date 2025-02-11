using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProjectV2.Models.DTO;
using ProjectV2.Models.Entities;


namespace ProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        private readonly IMapper _mapper;

        public ImageController(MedicalSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return _mapper.Map<List<ImageDto>>(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return _mapper.Map<ImageDto>(image);
        }

        [HttpPost]
        public async Task<ActionResult<ImageDto>> PostImage([FromForm] ImageUploadDto imageUploadDto)
        {
            if (imageUploadDto.ImageFile == null || imageUploadDto.ImageFile.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await imageUploadDto.ImageFile.CopyToAsync(memoryStream);

            var image = new Image
            {
                ImageData = memoryStream.ToArray(),
                CheckupId = imageUploadDto.CheckupId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = image.ImageId }, _mapper.Map<ImageDto>(image));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

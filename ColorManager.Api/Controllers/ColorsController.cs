using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColorManager.Api.Db;
using ColorManager.Api.Models;
using ColorManager.Api.DTO;

namespace ColorManager.Api.Controllers
{
    [Route("api/colors")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly CmDbContext _context;

        public ColorsController(CmDbContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorDTO>>> GetColorItems()
        {
            return await _context.ColorItems.Select(x => ColorModelToDTO(x)).ToListAsync();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorDTO>> GetColor(int id)
        {
            var color = await _context.ColorItems.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            return ColorModelToDTO(color);
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, ColorDTO color)
        {
            if (id != color.Id)
            {
                return BadRequest();
            }

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        // POST: api/Colors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(ColorDTO color)
        {
            var _color = new Color { Id = color.Id, Name = color.Name, HexCode = color.HexCode };

            _context.ColorItems.Add(_color);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColor), new { id = _color.Id }, _color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _context.ColorItems.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }

            _context.ColorItems.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorExists(int id)
        {
            return _context.ColorItems.Any(e => e.Id == id);
        }

        private static ColorDTO ColorModelToDTO(Color color) =>
            new ColorDTO
            {
                Id = color.Id,
                Name = color.Name,
                HexCode = color.HexCode
            };
    }
}

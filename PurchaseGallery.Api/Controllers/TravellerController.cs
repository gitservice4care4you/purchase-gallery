using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Models.travellers;
using PurchaseGallery.ApiService.Data;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TravellersController : ControllerBase
    {
        private readonly PurchaseGalleryDbContext _context;

        public TravellersController(PurchaseGalleryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traveller>>> GetTravellers()
        {
            return await _context.Travellers.Include(t => t.FromWhere).Include(t => t.ToWhere).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Traveller>> GetTraveller(Guid id)
        {
            var traveller = await _context.Travellers.Include(t => t.FromWhere).Include(t => t.ToWhere).FirstOrDefaultAsync(t => t.Id == id);

            if (traveller == null)
            {
                return NotFound();
            }

            return traveller;
        }

        [HttpPost]
        public async Task<ActionResult<Traveller>> PostTraveller(Traveller traveller)
        {
            _context.Travellers.Add(traveller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraveller", new { id = traveller.Id }, traveller);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraveller(Guid id, Traveller traveller)
        {
            if (id != traveller.Id)
            {
                return BadRequest();
            }

            _context.Entry(traveller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravellerExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraveller(Guid id)
        {
            var traveller = await _context.Travellers.FindAsync(id);
            if (traveller == null)
            {
                return NotFound();
            }

            _context.Travellers.Remove(traveller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravellerExists(Guid id)
        {
            return _context.Travellers.Any(e => e.Id == id);
        }
    }
}
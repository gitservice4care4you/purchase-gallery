using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.ApiService.Data;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PurchaseRequestController : ControllerBase
    {
        private readonly PurchaseGalleryDbContext _context;

        public PurchaseRequestController(PurchaseGalleryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRequest>>> GetPurchaseRequests()
        {

            return await _context.PurchaseRequests.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequest>> GetPurchaseRequest(Guid id)
        {
            var purchaseRequest = await _context.PurchaseRequests.Where(pr => pr.Id == id).FirstOrDefaultAsync();

            if (purchaseRequest == null)
            {
                return NotFound();
            }
            var assets = await _context.Asset
                .Where(a => purchaseRequest.AssetIds.Contains(a.Id))
                .ToListAsync();
            purchaseRequest.Assets = assets;

            return purchaseRequest;
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseRequest>> PostPurchaseRequest(PurchaseRequest purchaseRequest)
        {
            _context.PurchaseRequests.Add(purchaseRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseRequest", new { id = purchaseRequest.Id }, purchaseRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRequest(Guid id, PurchaseRequest purchaseRequest)
        {
            if (id != purchaseRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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
        public async Task<IActionResult> DeletePurchaseRequest(Guid id)
        {
            var purchaseRequest = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            _context.PurchaseRequests.Remove(purchaseRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(Guid id)
        {
            return _context.PurchaseRequests.Any(e => e.Id == id);
        }
    }
}

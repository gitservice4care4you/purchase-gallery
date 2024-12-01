using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Dtos.Asset;
using PurchaseGallery.Api.Mappers;
using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.ApiService.Data;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AssetController : ControllerBase
    {
        private readonly PurchaseGalleryDbContext _context;

        public AssetController(PurchaseGalleryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Asset.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(Guid id)
        {
            var asset = await _context.Asset.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model state", error = ModelState });
            }
            AssetCategory? assetCategory = await _context.AssetCategory.FirstOrDefaultAsync(a => a.Id == assetDto.AssetCategoryId);
            AssetType? assetType = await _context.AssetType.FirstOrDefaultAsync(a => a.Id == assetDto.AssetTypeId);
            if (assetCategory == null || assetType == null)
            {
                return BadRequest();
            }
            Asset assetTemp = AssetMapper.ToModel(assetDto, assetCategory, assetType);
            _context.Asset.Add(assetTemp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsset", new { id = assetTemp.Id }, assetTemp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(Guid id, AssetDto asset)
        {


            _context.Entry(asset).State = EntityState.Modified;

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
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var asset = await _context.Asset.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(Guid id)
        {
            return _context.Asset.Any(e => e.Id == id);
        }
    }
}

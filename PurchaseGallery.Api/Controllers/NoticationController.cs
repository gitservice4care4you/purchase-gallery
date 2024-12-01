using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Dtos.Notification;
using PurchaseGallery.Api.Mappers;
using PurchaseGallery.Api.Models.Notifications;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.ApiService.Data;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class NoticationController : ControllerBase
    {
        public PurchaseGalleryDbContext _context;

        NoticationController(PurchaseGalleryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notifications>>> GetNotications()
        {
            return await _context.Notifications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notifications>> GetNotication(Guid id)
        {
            var notication = await _context.Notifications.FindAsync(id);

            if (notication == null)
            {
                return NotFound();
            }

            return notication;
        }

        [HttpPost]
        public async Task<ActionResult<Notifications>> PostNotication(NotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PurchaseRequest? purchaseRequest = await _context.PurchaseRequests.FindAsync(notificationDto.PurchaseRequestId);

            if (purchaseRequest == null)
            {
                return BadRequest();
            }

            Notifications notification = NotificationMapper.ToModel(notificationDto, purchaseRequest);
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotication", new { id = notification.Id }, notification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotication(Guid id, Notifications notication)
        {
            if (id != notication.Id)
            {
                return BadRequest();
            }

            _context.Entry(notication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticationExists(id))
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
        public async Task<ActionResult<Notifications>> DeleteNotication(Guid id)
        {
            var notication = await _context.Notifications.FindAsync(id);
            if (notication == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notication);
            await _context.SaveChangesAsync();

            return notication;
        }

        private bool NoticationExists(Guid id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }

    }
}

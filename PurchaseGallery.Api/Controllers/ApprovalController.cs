using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Dtos.Approval;
using PurchaseGallery.Api.Mappers;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.ApiService.Data;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApprovalController : ControllerBase
    {
        private readonly PurchaseGalleryDbContext _context;

        public ApprovalController(PurchaseGalleryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Approval>>> GetApprovals()
        {
            return await _context.Approvals.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Approval>> GetApproval(Guid id)
        {
            var approval = await _context.Approvals.FindAsync(id);

            if (approval == null)
            {
                return NotFound();
            }

            return approval;
        }

        [HttpPost]
        public async Task<ActionResult<Approval>> PostApproval(ApprovalDto approvalDto)
        {
            if (approvalDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User? approver = await _context.Users.FindAsync(approvalDto.ApproverId);
            PurchaseRequest? purchaseRequest = await _context.PurchaseRequests.FindAsync(approvalDto.PurchaseRequestId);
            if (approver == null || purchaseRequest == null)
            {
                return BadRequest();
            }

            Approval approvalModel = ApprovalMapper.ToModel(approvalDto, purchaseRequest, approver);

            _context.Approvals.Add(approvalModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApproval", new { id = approvalModel.Id }, approvalModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApproval(Guid id, Approval approval)
        {
            if (id != approval.Id)
            {
                return BadRequest();
            }

            _context.Entry(approval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovalExists(id))
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
        public async Task<IActionResult> DeleteApproval(Guid id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            if (approval == null)
            {
                return NotFound();
            }

            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApprovalExists(Guid id)
        {
            return _context.Approvals.Any(e => e.Id == id);
        }
    }
}

// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.SqlClient;
// using Microsoft.EntityFrameworkCore;
// using PurchaseGallery.Api.Dtos.Users;
// using PurchaseGallery.Api.Mappers;
// using PurchaseGallery.ApiService.Data;
// using PurchaseGallery.ApiService.Models;
// using PurchaseGallery.ApiService.Models.Auth;

// namespace PurchaseGallery.ApiService.Controllers
// {

//     [Route("api/users")]
//     [ApiController]
//     public class UsersController : ControllerBase
//     {
//         public readonly PurchaseGalleryDbContext _context;



//         public UsersController(PurchaseGalleryDbContext context) => _context = context;

//         // GET: api/Users
//         [HttpGet]
//         [Authorize(AuthenticationSchemes = "Bearer")]
//         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//         {
//             List<User> usersList = await _context.Users.ToListAsync();
//             if (usersList.Count == 0)
//             {
//                 return NotFound(new { Message = "No Users found" });
//             }
//             return usersList;
//         }
//         // GET: api/Users/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<User>> GetUser(int id)
//         {
//             var user = await _context.Users.FindAsync(id);

//             if (user == null)
//             {
//                 return NotFound(new { Message = "User not found" });
//             }

//             return user;
//         }
//         // PUT: api/Users/5
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutUser(Guid id, User user)
//         {
//             if (id != user.Id)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(user).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!UserExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return NoContent();
//         }

//         // POST: api/Users
//         [HttpPost]
//         public async Task<ActionResult<User>> PostUser(LoginRegisterUserDto userDto)
//         {
//             if (!ModelState.IsValid)
//             {
//                 var errors = ModelState.Values.SelectMany(v => v.Errors)
//                                   .Select(e => e.ErrorMessage)
//                                   .ToList();
//                 var errorMessage = string.Join("; ", errors);
//                 return BadRequest(new { Message = "Validation failed", Errors = errorMessage.Remove(0).Remove(4) });
//             }

//             User user = UsersMappers.MapLoginRegisterUserDtoToUser(userDto);

//             _context.Users.Add(user);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction("GetUser", new { id = user.Id }, user);
//         }

//         // DELETE: api/Users/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteUser(int id)
//         {
//             var user = await _context.Users.FindAsync(id);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             _context.Users.Remove(user);
//             await _context.SaveChangesAsync();

//             return NoContent();
//         }

//         private bool UserExists(Guid id)
//         {
//             return _context.Users.Any(e => e.Id == id);
//         }

//     }
// }



using Microsoft.AspNetCore.Mvc;
using PurchaseGallery.ApiService.Models;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.ApiService.Data;
using PurchaseGallery.ApiService.Models.Auth;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(PurchaseGalleryDbContext context) : ControllerBase
    {
        private readonly PurchaseGalleryDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.Include(u => u.Id).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
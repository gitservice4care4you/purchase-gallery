using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PurchaseGallery.ApiService.Data;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.Api.Models;
using PurchaseGallery.Api.Dtos.Users;
using PurchaseGallery.Api.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController(PurchaseGalleryDbContext context, IConfiguration configuration) : ControllerBase
    {
        /* ----------------------------- local variables ---------------------------- */
        public readonly PurchaseGalleryDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;


        /* -------------------------------------------------------------------------- */
        /*                                  Endpoints                                 */
        /* -------------------------------------------------------------------------- */

        /* ---------------------------------- login --------------------------------- */
        /// <summary>
        /// Authenticates a user based on the provided login details.
        /// </summary>
        /// <param name="userDto">The login details provided by the client.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult:
        /// - 200 OK with a token if the login is successful.
        /// - 400 Bad Request if the login details are invalid.
        /// - 404 Not Found if the user is not found.
        /// </returns>
        /// <response code="200">Returns a token if the login is successful.</response>
        /// <response code="400">If the login details are invalid.</response>
        /// <response code="404">If the user is not found.</response>
        [Route("login"), HttpPost]
        public async Task<IActionResult> Login(LoginRegisterUserDto userDto)
        {
            // Check if the login details are valid
            if (userDto == null)
            {
                return BadRequest(new { message = "Invalid client request" });

            }
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {

                return BadRequest(new { message = "Invalid model state", error = ModelState });
            }

            User? user = await _context.Users.SingleOrDefaultAsync(u => u.AzureAdId == userDto.AzureAdId);
            // Check if the user exists
            if (user == null)
            {
                var userCreateProcess = await CreateUser(createUserDto: userDto);
            }

            // Generate a JWT token string for the user
            string tokenString = GetTokenString(UsersMappers.MapLoginRegisterUserDtoToUser(userDto));
            // Return the token string
            return Ok(new ApiResponse(200, "Success", new
            {
                Token = tokenString,
                userDetails = new
                {
                    userDto.AzureAdId,
                    userDto.FullName,
                    userDto.Email,
                    userDto.Country,
                    userDto.Department,
                    userDto.JobTitle
                },
            }));
        }


        /* -------------------------------- register -------------------------------- */
        /// <summary>
        /// Registers a new user based on the provided registration details.
        /// </summary>
        /// <param name="createUserDto">The registration details provided by the client.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult:
        /// - 201 Created if the registration is successful.
        /// - 400 Bad Request if the registration details are invalid.
        /// </returns>
        /// <response code="201">Returns a success message if the registration is successful.</response>
        /// <response code="400">If the registration details are invalid.</response>
        [Route("register"), HttpPost]
        public async Task<IActionResult> Register(LoginRegisterUserDto createUserDto)
        {
            var result = await CreateUser(createUserDto);
            if (result is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }

            return CreatedAtAction(nameof(Register), new ApiResponse(201, "User registered successfully"));
        }








        /* ----------------------------- local functions ----------------------------- */

        /// <summary>
        /// Generates a JWT token string for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <returns>A JWT token string.</returns>
        private string GetTokenString(User user)
        {

            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.AzureAdId),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Name, user.Email),

            ];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }


        /// <summary>
        /// Creates a new user based on the provided registration details.
        /// </summary>
        /// <param name="createUserDto">The registration details provided by the client.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an IActionResult:
        /// - 201 Created if the registration is successful.
        /// - 400 Bad Request if the registration details are invalid.
        /// </returns>
        private async Task<IActionResult> CreateUser(LoginRegisterUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UsersMappers.MapLoginRegisterUserDtoToUser(createUserDto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new OkResult();
        }




    }
}
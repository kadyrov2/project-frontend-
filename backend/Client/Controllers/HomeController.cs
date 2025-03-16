using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Services;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiService _apiService;

        public UsersController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _apiService.GetUsersAsync();
            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _apiService.AddUserAsync(user);

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}
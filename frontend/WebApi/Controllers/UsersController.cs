using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        const string BACKEND_HOST = "https://localhost:5000";

        //UsersContext db;
        //public UsersController(UsersContext context)
        //{
        //    db = context;
        //    if (!db.Users.Any())
        //    {
        //        db.Users.Add(new User { Name = "Tom", Email = "26" });
        //        db.Users.Add(new User { Name = "Alice", Email = "31" });
        //        db.SaveChanges();
        //    }
        //}

        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // Пример запроса к внешнему API
            var response = await _httpClient.GetAsync($"{BACKEND_HOST}/api/users");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content); // Возвращаем данные от внешнего API
            }

            return StatusCode((int)response.StatusCode, "Ошибка при запросе к внешнему API");
        }


        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var response = await _httpClient.GetAsync($"{BACKEND_HOST}/api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                if (user == null) return NotFound();
                return new ObjectResult(user);
            }

             return StatusCode((int)response.StatusCode, "Ошибка при запросе к внешнему API");
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var json = System.Text.Json.JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BACKEND_HOST}/api/users", data);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User created successfully!");
            }
  
            return Ok(user);
        }

        //// PUT api/users/
        //[HttpPut]
        //public async Task<ActionResult<User>> Put(User user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.Users.Any(x => x.Id ==user.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(user);
        //    await db.SaveChangesAsync();
        //    return Ok(user);
        //}

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {

            var response = await _httpClient.DeleteAsync($"{BACKEND_HOST}/api/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User deleted successfully!");
            }
            var user = Get(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}

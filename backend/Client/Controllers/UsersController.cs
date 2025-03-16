using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Services;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;
         HttpClient _httpClient = new HttpClient();

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            // Получаем список пользователей
            var users = await _apiService.GetUsersAsync();
            return View(users);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {

                var json = JsonSerializer.Serialize(new
                {
                    name = user.Name,
                    email = user.Email,
                    id = Guid.NewGuid().ToString()
            });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5000/api/users", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Success"); // Перенаправление на страницу успеха
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при отправке данных на сервер.");
                }

                // Добавляем пользователя через сервис
                await _apiService.AddUserAsync(user);

                // Перенаправляем на страницу Index, чтобы отобразить обновленный список
                return RedirectToAction(nameof(Index));

            }

            // Если данные невалидны, возвращаем представление с ошибками
            return View(user);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Services
{
	public class ApiService
	{
		private static List<User> _users = new List<User>();

		public async Task<List<User>> GetUsersAsync()
		{
			// Возвращаем список пользователей
			return await Task.FromResult(_users);
		}

		public async Task AddUserAsync(User user)
		{
			// Генерация ID (в реальном приложении это делается через базу данных)
			user.Id = Guid.NewGuid().ToString(); 

			// Добавляем пользователя в список
			_users.Add(user);

			await Task.CompletedTask;
		}
	}
}
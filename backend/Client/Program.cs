using Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ApiService>(); // Регистрация ApiService
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseStaticFiles();
// Настройка маршрутов
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Create}/{id?}");

app.Run();
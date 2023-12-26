using Microsoft.EntityFrameworkCore;
using SportsClubBackEnd.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:DefaultConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
});
app.Run();
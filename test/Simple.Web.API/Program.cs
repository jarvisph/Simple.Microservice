var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();

var app = builder.Build();


app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:5000");

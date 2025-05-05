using CRM.API.Middlewares;
using CRM.Repositories;
using CRM.Service;
using CRM.Service.Implementation.Message;
using CRM.Service.Interface.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        $@"Data Source={AppDbContext.USER_FOLDER}/NETCRM.db;",
        b => b.MigrationsAssembly("CRM.API") 
    ));
builder.Services.AddMemoryCache();
builder.Services.RepositoryInjection();
builder.Services.ServiceInjection();

builder.Services.AddControllers();

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRM Sales API",
        Version = "1.0.0"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM Sales API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.UseGlobalExceptionHandling(); 

app.Run();

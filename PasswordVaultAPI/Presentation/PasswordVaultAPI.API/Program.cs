using PasswordVaultAPI.API.Logs;
using PasswordVaultAPI.Application;
using PasswordVaultAPI.Persistence;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();

builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin() 
                          .AllowAnyMethod() 
                          .AllowAnyHeader()); 
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var log = LogConfig.CreateLogConfig(builder.Configuration);

builder.Host.UseSerilog(log);


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();


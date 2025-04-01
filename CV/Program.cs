using CV.Controllers;
using CV.Data;
using CV.Models;
using CV.Repository;
using Microsoft.EntityFrameworkCore;
using static CV.Repository.Education;
using static CV.Repository.Project;
using Language = CV.Repository.Language;
using Project = CV.Repository.Project;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddScoped<ILanguage,Language>();
builder.Services.AddScoped<IProject, Project>();
builder.Services.AddScoped<IResume, Resume>();
builder.Services.AddScoped<IEducation, EducationRepo>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
        .WithOrigins(
                "https://localhost:3000",  // Your Next.js dev server
                "http://localhost:3000",   // Fallback for HTTP
                "https://ibrahimasecka-fvhxg3a8dkegetd4.westeurope-01.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod());

});
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var environment = builder.Environment.EnvironmentName;
var connectionString = builder.Configuration.GetConnectionString(
    environment == "Development" ? "DevDatabase" : "ProdDatabase"
);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

Console.WriteLine($"Using DB Connection: {connectionString}");



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();

}
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

app.configureIbbiController();
app.configureEducationController();
app.configureLanguageController();
app.configureProjectController();
app.configureResumeController();
app.UseHttpsRedirection(); // Should come early

app.UseRouting(); // Must come before CORS

// Add CORS middleware here - AFTER UseRouting() but BEFORE UseAuthorization()
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
Console.WriteLine($"DB Connection: {connectionString}");
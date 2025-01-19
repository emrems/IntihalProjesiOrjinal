using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Repositories.Ef_core;
using IntihalProjesi.Services.Contracts;
using IntihalProjesi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext Kaydı
builder.Services.AddDbContext<OrjinalIntihalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Kaydı
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();

// Service Kaydı
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IKullaniciService, KullaniciManager>();


builder.Services.AddAutoMapper(typeof(Program));

// CORS Policy Tanımı
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173") // Vue.js projesinin çalıştığı port
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

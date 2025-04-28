using System;
using System.Security.Claims;
using System.Text;
using IntihalProjesi.Helpers;
using IntihalProjesi.Helpers.Contract;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Repositories.Ef_core;
using IntihalProjesi.Services;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<OrjinalIntihalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository registrations
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IIcerikRepository, IcerikRepository>();
builder.Services.AddScoped<IDosyaRepository, DosyaRepository>();
builder.Services.AddScoped<IBenzerlikSonuclariRepository, BenzerlikSonuclariRepository>();
builder.Services.AddScoped<IBildirimRepository, BildirimRepository>();

// Service registrations
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IKullaniciService, KullaniciManager>();
builder.Services.AddScoped<IIcerikService, IcerikManager>();
builder.Services.AddScoped<IDosyaService, DosyaManager>();
builder.Services.AddScoped<IBenzerlikSonucuService, BenzerlikSonucuManager>();
builder.Services.AddScoped<IBildirimService, BildirimManager>();

// JWT helper
builder.Services.AddScoped<IJwtHelper, JwtHelper>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Authentication & JWT configuration
var jwtSection = builder.Configuration.GetSection("JWT");
var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Key"]);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,            // <–– Token süresi tam dolduğunda geçersiz olsun

            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

// HttpContext access 
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

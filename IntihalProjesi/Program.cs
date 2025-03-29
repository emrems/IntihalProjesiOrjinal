using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Repositories.Ef_core;
using IntihalProjesi.Services.Contracts;
using IntihalProjesi.Services;
using Microsoft.EntityFrameworkCore;
using IntihalProjesi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IntihalProjesi.Helpers.Contract;

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
builder.Services.AddScoped<IIcerikRepository, IcerikRepository>();
builder.Services.AddScoped<IDosyaRepository, DosyaRepository>();
builder.Services.AddScoped<IBenzerlikSonuclariRepository, BenzerlikSonuclariRepository>();

// Service Kaydı
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IKullaniciService, KullaniciManager>();
builder.Services.AddScoped<IIcerikService, IcerikManager>();
builder.Services.AddScoped<IDosyaService, DosyaManager>();
builder.Services.AddScoped<IBenzerlikSonucuService, BenzerlikSonucuManager>();


// JWT Service Kaydı
builder.Services.AddScoped<IJwtHelper, JwtHelper>();

// AutoMapper Kaydı
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS Policy Tanımı
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// JWT Ayarlarını Yükle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JWT");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});
// HttpContext'e erişim için hizmet ekleme
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

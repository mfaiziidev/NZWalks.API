using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mapping;
using NZWalks.API.Repositries;
using NZWalks.API.Repositries.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setting up DbContext For Data
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Setting up DbContext for Authentication
builder.Services.AddDbContext<NZWalkAuthContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Setting up Repositories
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<IDifficultyRepository, SQLDifficultyRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

// Setting up Auto Mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Setting up Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalkAuthContext>()
    .AddDefaultTokenProviders();

// Setting up Password Format
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;

});

// Setting up Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    throw new InvalidOperationException("JWT configuration values are missing or incorrect.");
}
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], 
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

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

using Microsoft.EntityFrameworkCore;
using PurchaseGallery.ApiService.Data;
using Microsoft.AspNetCore.Authentication.BearerToken;
using PurchaseGallery.ApiService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
var JwtBearer = builder.Configuration.GetSection("Jwt");


builder.AddServiceDefaults();


// Add services to the container.

builder.Services.AddControllers();


// Connect to Server for DB
builder.Services.AddDbContext<PurchaseGalleryDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddAuthorization();
var issuer = JwtBearer["Issuer"];
Console.WriteLine($"Issuer: {issuer}"); // This should print "YourIssuerValue"

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtBearer["Issuer"],
            ValidAudience = JwtBearer["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtBearer["Key"]!))
        };
    });

// builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddDefaultUI().AddEntityFrameworkStores<PurchaseGalleryDbContext>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
// Add authentication


app.MapControllers();

app.Run();

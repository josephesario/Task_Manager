using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Task_Manager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("taskManagerDbContext");
builder.Services.AddDbContext<taskManagerDbContext>(options => options.UseSqlServer(connectionString));

// Configure authentication
var tokenSecretKey = builder.Configuration["Jwt:SecretKey"]; // Replace with your secret key
var tokenIssuer = builder.Configuration["Jwt:Issuer"]; // Replace with your token issuer
var tokenAudience = builder.Configuration["Jwt:Audience"]; // Replace with your token audience

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecretKey)),
            ValidateIssuer = true,
            ValidIssuer = tokenIssuer,
            ValidateAudience = true,
            ValidAudience = tokenAudience,
            ClockSkew = TimeSpan.Zero
        };
    });

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

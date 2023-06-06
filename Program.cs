using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
using BrodyagaWeb.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BrodyagaWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BrodyagaWebContext") ??
        throw new InvalidOperationException("Connection string 'BrodyagaWebContext' not found.")));

//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//var secretKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
//var issure = builder.Configuration.GetSection("JwtSettings:Issure").Value;
//var audience = builder.Configuration.GetSection("JwtSettings:Audience").Value;
//var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = issure,
//            ValidAudience = audience,
//            IssuerSigningKey = signingKey
//        };
//    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();

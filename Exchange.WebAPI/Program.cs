using Exchange.BAL.Services.AppConfiguration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Exchange.BAL.Services;
using System.Text.Json.Serialization;
using Exchange.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region JwtSetting
var jwtSetting = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
builder.Services.AddScoped<JwtSetting>();
#endregion


#region API-Health-Check
builder.Services.AddHealthChecks();
#endregion


builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



#region Register-Custom-Services
builder.Services.AddRegisterBusinessServices();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#endregion



#region Adding-Authentication-Scheme

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(options =>
//{
//    options.Cookie.Name = "YourCookieName";
//    options.LoginPath = "/Account/Login"; // Set the login path
//    options.AccessDeniedPath = "/Account/AccessDenied"; // Set the access denied path
//});
#endregion

#region Adding-Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = jwtSetting.ValidIssuer,
               ValidAudience = jwtSetting.ValidAudience,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.IssuerSigningKey)),
               //  ClockSkew = TimeSpan.FromMinutes(10),
               RequireExpirationTime = true,
               ClockSkew = TimeSpan.Zero
           };
       });
#endregion


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    "Data Source=.;Initial Catalog=BarterApp;Integrated Security=True;Encrypt=False"

    ));


var app = builder.Build();

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

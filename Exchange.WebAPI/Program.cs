
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
using Exchange.DAL;
using Exchange.BAL.Services.Repositories;
using Exchange.BAL.Services.Contracts;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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


#region Identity and User setting with third-party 
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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


#region Swagger Header for Authorization 
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        BearerFormat = "JWT",
    });

    // Add a new parameter named 'ApplicationID'
    options.AddSecurityDefinition("applicationId", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "ApplicationID",
        Type = SecuritySchemeType.ApiKey,
        Description = "Application ID",
        BearerFormat = "ApplicationID",
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#endregion
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

#region Json setting

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
#endregion

#region ConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarterAppConnectionString")));
#endregion 


//builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//  .AddEntityFrameworkStores<ApplicationDbContext>();
#region Register-Custom-Services
builder.Services.AddRegisterBusinessServices();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<ApplicationUser>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

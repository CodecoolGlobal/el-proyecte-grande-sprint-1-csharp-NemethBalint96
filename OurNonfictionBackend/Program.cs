using ElProyecteGrande.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OurNonfictionBackend.Auth;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Data;
using OurNonfictionBackend.Models;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://our-nonfiction.herokuapp.com/", "https://our-nonfiction.herokuapp.com/registration").AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddDbContext<NonfictionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionSqlite")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var tokenKey = Environment.GetEnvironmentVariable("TokenKey");
var key = Encoding.ASCII.GetBytes(tokenKey);
var clientId = Environment.GetEnvironmentVariable("ClientId");

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
var accountService = serviceProvider.GetService<IAccountService>();

builder.Services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey, accountService, clientId));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

app.Run($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");
using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Dal.Repositories;
using OurNonfictionBackend.Data;
using OurNonfictionBackend.Models;

//var Configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NonfictionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Room>, RoomRepository>();
builder.Services.AddScoped<IRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<IBookingDetailsService>(x => new BookingDetailsService(x.GetRequiredService<IRepository<Booking>>(), x.GetRequiredService<IRepository<Room>>()));
builder.Services.AddScoped<IBookingService>(x => new BookingService(x.GetRequiredService<IRepository<Booking>>()));
builder.Services.AddScoped<IRoomService>(x => new RoomService(x.GetRequiredService<IRepository<Room>>()));

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

app.CreateDbIfNotExists();

app.Run();

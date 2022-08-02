using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;
using OurNonfictionBackend.Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Room>>(new RoomRepository());
builder.Services.AddSingleton<IRepository<Booking>>(x => new BookingRepository(x.GetRequiredService<IRepository<Room>>()));
builder.Services.AddSingleton<IBookingDetailsService>(x => new BookingDetailsService(x.GetRequiredService<IRepository<Booking>>(), x.GetRequiredService<IRepository<Room>>()));
builder.Services.AddSingleton<IBookingService>(x => new BookingService(x.GetRequiredService<IRepository<Booking>>()));
builder.Services.AddSingleton<IRoomService>(x => new RoomService(x.GetRequiredService<IRepository<Room>>()));

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

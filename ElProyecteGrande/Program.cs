using ElProyecteGrande.Dal;
using ElProyecteGrande.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRepository<Room>>(new RoomRepository());
builder.Services.AddSingleton<IRepository<Booking>>(x => new BookingRepository(x.GetRequiredService<IRepository<Room>>()));
builder.Services.AddSingleton<IBookingService>(x => new BookingService(x.GetRequiredService<IRepository<Booking>>()));
builder.Services.AddSingleton<IRoomService>(x => new RoomService(x.GetRequiredService<IRepository<Room>>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Bookings}/{id?}");

app.Run();

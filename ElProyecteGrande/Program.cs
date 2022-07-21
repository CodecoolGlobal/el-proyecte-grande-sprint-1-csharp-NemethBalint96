using ElProyecteGrande.Dao;
using ElProyecteGrande.Data;
using ElProyecteGrande.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<BookingDaoMemory>(BookingDaoMemory.GetInstance());
builder.Services.AddSingleton<RoomDaoMemory>(RoomDaoMemory.GetInstance());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


Dataseed.SetupInMemoryDatabase(BookingDaoMemory.GetInstance());

app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Bookings}/{id?}");

app.Run();

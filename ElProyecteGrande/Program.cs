using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<BookingDaoMemory>(BookingDaoMemory.GetInstance());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

void SetupInMemoryDatabase()
{
    BookingDaoMemory.GetInstance().Add(new Booking
    {
        Adults = 1,
        ArrivalDate = DateOnly.FromDateTime(DateTime.Now),
        DepartureDate = DateOnly.Parse("2022.07.23"),
        BookersName = "Németh Bálint",
        Children = 2,
        Country = "Hungary",
        Created = DateTime.Now,
        Email = "nemeth.balint1996@gmail.com",
        Guests = new List<Guest>(),
        ID = 1,
        Room = new Room
        {
            Comment = "  ",
            Floor = 1,
            Name = "Hehe",
            Id = 1
        },
        Total =5000,
        Infants = 0,
        Status = Status.Confirmed,
    });

}

SetupInMemoryDatabase();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

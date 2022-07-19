using ElProyecteGrande.Dao;
using ElProyecteGrande.Models;



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
        ArrivalDate = DateOnly.FromDateTime(DateTime.Now),
        DepartureDate = DateOnly.Parse("2022.07.23"),
        BookersName = "Németh Bálint",
        Country = "Hungary",
        Created = DateTime.Now,
        Email = "nemeth.balint1996@gmail.com",
        Guests = new List<Guest>{
            new Guest
            {
                FullName = "Németh Bálint",
                BirthDate = DateOnly.Parse("1996.01.01"),
                BirthPlace = "Eger",
                Email = "nemeth.balint1996@gmail.com",
                Phone = "00000000",
                Country = "Hungary",
                City = "Füzesabony",
                Adress = "",
                PostalCode = 3390,
                Citizenship = "Hungary",
            },
            new Guest
            {
                BirthDate = DateOnly.Parse("2022.01.01"),
            }
        },
        ID = 1,
        Room = new Room
        {
            Comment = "  ",
            Floor = 1,
            Name = "Hehe",
            ID = 1,
            Price = 20
        },
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
    pattern: "{controller=Home}/{action=Bookings}/{id?}");

app.Run();

using ElProyecteGrande.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ElProyecteGrande.Dao;

namespace ElProyecteGrande.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookingDaoMemory _bookingDaoMemory;

    public HomeController(ILogger<HomeController> logger,BookingDaoMemory booking)
    {
        _logger = logger;
        _bookingDaoMemory= booking;
    }

    public IActionResult Index()
    {
        var books = _bookingDaoMemory.GetAll();
        return View(books);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
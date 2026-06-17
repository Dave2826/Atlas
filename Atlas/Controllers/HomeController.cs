using Atlas.Data;
using Atlas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Atlas.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AtlasDbContext _context;

    public HomeController(AtlasDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userCount = _context.Users.Count();
        var productCount = _context.Products.Count();
        var customerCount = _context.Customers.Count();
        var currentUser = User.Identity?.Name;
        var currentRole = User.FindFirst(ClaimTypes.Role)?.Value;

        ViewBag.UserCount = userCount;
        ViewBag.ProductCount = productCount;
        ViewBag.CustomerCount = customerCount;
        ViewBag.CurrentUser = currentUser;
        ViewBag.CurrentRole = currentRole;

        return View();
    }

    [AllowAnonymous]
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

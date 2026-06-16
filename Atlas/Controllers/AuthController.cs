using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Atlas.Data;
using Atlas.Models.Entities;
using Atlas.Models.ViewModels;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

public class AuthController : Controller
{
    private readonly AtlasDbContext _context;

    public AuthController(AtlasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == model.Username);

        Console.WriteLine($"[DEBUG] User found: {user != null}");
        if (user != null)
        {
            Console.WriteLine($"[DEBUG] Username: {user.Username}");
            Console.WriteLine($"[DEBUG] IsActive: {user.IsActive}");
            Console.WriteLine($"[DEBUG] PasswordHash length: {user.PasswordHash?.Length ?? 0}");
            
            var testVerify = BCrypt.Net.BCrypt.Verify("Atlas2026", user.PasswordHash);
            Console.WriteLine($"[DEBUG] Test verification result (Atlas2026): {testVerify}");
        }

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Usuario inactivo.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var identity = new ClaimsIdentity(claims, 
            Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(principal);

        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}
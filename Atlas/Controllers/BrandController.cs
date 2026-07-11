using Atlas.Data;
using Atlas.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

[Authorize]
public class BrandController : Controller
{
    private readonly AtlasDbContext _context;

    public BrandController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? statusFilter)
    {
        var query = _context.Brands.AsQueryable();

        if (string.IsNullOrEmpty(statusFilter) || statusFilter == "Activos")
            query = query.Where(b => b.IsActive);
        else if (statusFilter == "Inactivos")
            query = query.Where(b => !b.IsActive);

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(b => EF.Functions.ILike(b.Name, $"%{searchTerm}%"));

        query = query.OrderBy(b => b.Name);

        var brands = await query.ToListAsync();

        ViewBag.TotalCount = await _context.Brands.CountAsync();
        ViewBag.ActiveCount = await _context.Brands.CountAsync(b => b.IsActive);
        ViewBag.InactiveCount = await _context.Brands.CountAsync(b => !b.IsActive);
        ViewBag.SearchTerm = searchTerm;
        ViewBag.StatusFilter = statusFilter ?? "Activos";

        return View(brands);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand brand)
    {
        brand.Name = brand.Name.Trim();

        if (await _context.Brands.AnyAsync(b => EF.Functions.ILike(b.Name, brand.Name)))
            ModelState.AddModelError("Name", "Ya existe una marca con ese nombre.");

        if (ModelState.IsValid)
        {
            _context.Add(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(brand);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == id);

        if (brand == null)
            return NotFound();

        return View(brand);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var brand = await _context.Brands.FindAsync(id);

        if (brand == null)
            return NotFound();

        return View(brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Brand brand)
    {
        if (id != brand.BrandId)
            return NotFound();

        brand.Name = brand.Name.Trim();

        if (await _context.Brands.AnyAsync(b => EF.Functions.ILike(b.Name, brand.Name) && b.BrandId != id))
            ModelState.AddModelError("Name", "Ya existe una marca con ese nombre.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(brand);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(brand.BrandId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(brand);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == id);

        if (brand == null)
            return NotFound();

        return View(brand);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand != null)
        {
            brand.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(int id)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand != null)
        {
            brand.IsActive = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool BrandExists(int id)
    {
        return _context.Brands.Any(e => e.BrandId == id);
    }
}

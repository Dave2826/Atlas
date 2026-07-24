using Atlas.Data;
using Atlas.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

[Authorize]
public class SizeController : Controller
{
    private readonly AtlasDbContext _context;

    public SizeController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? statusFilter)
    {
        var query = _context.Sizes.AsQueryable();

        if (string.IsNullOrEmpty(statusFilter) || statusFilter == "Activos")
            query = query.Where(s => s.IsActive);
        else if (statusFilter == "Inactivos")
            query = query.Where(s => !s.IsActive);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.Trim();
            query = query.Where(s => EF.Functions.ILike(s.Name, $"%{searchTerm}%"));
        }

        query = query.OrderBy(s => s.DisplayOrder).ThenBy(s => s.Name);

        var sizes = await query.ToListAsync();

        ViewBag.TotalCount = await _context.Sizes.CountAsync();
        ViewBag.ActiveCount = await _context.Sizes.CountAsync(s => s.IsActive);
        ViewBag.InactiveCount = await _context.Sizes.CountAsync(s => !s.IsActive);
        ViewBag.SearchTerm = searchTerm;
        ViewBag.StatusFilter = statusFilter ?? "Activos";

        return View(sizes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Size size)
    {
        size.Name = size.Name?.Trim() ?? string.Empty;

        if (await _context.Sizes.AnyAsync(s => EF.Functions.ILike(s.Name, size.Name)))
            ModelState.AddModelError("Name", "Ya existe una talla con ese nombre.");

        if (size.DisplayOrder < 0)
            ModelState.AddModelError("DisplayOrder", "El orden no puede ser negativo.");

        if (ModelState.IsValid)
        {
            _context.Add(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(size);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var size = await _context.Sizes.FirstOrDefaultAsync(s => s.SizeId == id);

        if (size == null)
            return NotFound();

        return View(size);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var size = await _context.Sizes.FindAsync(id);

        if (size == null)
            return NotFound();

        return View(size);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Size size)
    {
        if (id != size.SizeId)
            return NotFound();

        size.Name = size.Name?.Trim() ?? string.Empty;

        if (await _context.Sizes.AnyAsync(s => EF.Functions.ILike(s.Name, size.Name) && s.SizeId != id))
            ModelState.AddModelError("Name", "Ya existe una talla con ese nombre.");

        if (size.DisplayOrder < 0)
            ModelState.AddModelError("DisplayOrder", "El orden no puede ser negativo.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(size);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(size.SizeId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(size);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var size = await _context.Sizes.FirstOrDefaultAsync(s => s.SizeId == id);

        if (size == null)
            return NotFound();

        return View(size);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var size = await _context.Sizes.FindAsync(id);

        if (size != null)
        {
            size.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(int id)
    {
        var size = await _context.Sizes.FindAsync(id);

        if (size != null)
        {
            size.IsActive = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool SizeExists(int id)
    {
        return _context.Sizes.Any(e => e.SizeId == id);
    }
}
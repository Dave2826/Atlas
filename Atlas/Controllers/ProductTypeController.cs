using Atlas.Data;
using Atlas.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

[Authorize]
public class ProductTypeController : Controller
{
    private readonly AtlasDbContext _context;

    public ProductTypeController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? statusFilter)
    {
        var query = _context.ProductTypes.AsQueryable();

        if (string.IsNullOrEmpty(statusFilter) || statusFilter == "Activos")
            query = query.Where(pt => pt.IsActive);
        else if (statusFilter == "Inactivos")
            query = query.Where(pt => !pt.IsActive);

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(pt => EF.Functions.ILike(pt.Name, $"%{searchTerm}%"));

        query = query.OrderBy(pt => pt.Name);

        var productTypes = await query.ToListAsync();

        ViewBag.TotalCount = await _context.ProductTypes.CountAsync();
        ViewBag.ActiveCount = await _context.ProductTypes.CountAsync(pt => pt.IsActive);
        ViewBag.InactiveCount = await _context.ProductTypes.CountAsync(pt => !pt.IsActive);
        ViewBag.SearchTerm = searchTerm;
        ViewBag.StatusFilter = statusFilter ?? "Activos";

        return View(productTypes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductType productType)
    {
        productType.Name = productType.Name.Trim();

        if (await _context.ProductTypes.AnyAsync(pt => EF.Functions.ILike(pt.Name, productType.Name)))
            ModelState.AddModelError("Name", "Ya existe un tipo de producto con ese nombre.");

        if (ModelState.IsValid)
        {
            _context.Add(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(productType);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.ProductTypeId == id);

        if (productType == null)
            return NotFound();

        return View(productType);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType == null)
            return NotFound();

        return View(productType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductType productType)
    {
        if (id != productType.ProductTypeId)
            return NotFound();

        productType.Name = productType.Name.Trim();

        if (await _context.ProductTypes.AnyAsync(pt => EF.Functions.ILike(pt.Name, productType.Name) && pt.ProductTypeId != id))
            ModelState.AddModelError("Name", "Ya existe un tipo de producto con ese nombre.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(productType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(productType.ProductTypeId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(productType);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.ProductTypeId == id);

        if (productType == null)
            return NotFound();

        ViewBag.ProductCount = await _context.Products.CountAsync(p => p.ProductTypeId == id);

        return View(productType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType != null)
        {
            productType.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(int id)
    {
        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType != null)
        {
            productType.IsActive = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProductTypeExists(int id)
    {
        return _context.ProductTypes.Any(e => e.ProductTypeId == id);
    }
}

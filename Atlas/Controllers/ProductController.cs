using Atlas.Data;
using Atlas.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly AtlasDbContext _context;

    public ProductController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductType)
            .ToListAsync());
    }

    public IActionResult Create()
    {
        ViewBag.Brands = new SelectList(
            _context.Brands.ToList(),
            "BrandId",
            "Name");

        ViewBag.ProductTypes = new SelectList(
            _context.ProductTypes.ToList(),
            "ProductTypeId",
            "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Brands = new SelectList(
            _context.Brands.ToList(),
            "BrandId",
            "Name");

        ViewBag.ProductTypes = new SelectList(
            _context.ProductTypes.ToList(),
            "ProductTypeId",
            "Name");

        return View(product);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        ViewBag.Brands = new SelectList(
            _context.Brands.ToList(),
            "BrandId",
            "Name",
            product.BrandId);

        ViewBag.ProductTypes = new SelectList(
            _context.ProductTypes.ToList(),
            "ProductTypeId",
            "Name",
            product.ProductTypeId);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.ProductId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}

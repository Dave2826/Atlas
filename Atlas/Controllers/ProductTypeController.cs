using Atlas.Data;
using Atlas.Models.Entities;
using Atlas.Models.ViewModels;
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

    public async Task<IActionResult> ManageSizes(int id)
    {
        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType == null)
            return NotFound();

        var allSizes = await _context.Sizes
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Name)
            .ToListAsync();

        var associatedSizeIds = await _context.ProductTypeSizes
            .Where(pts => pts.ProductTypeId == id)
            .Select(pts => pts.SizeId)
            .ToListAsync();

        var viewModel = new ProductTypeSizeViewModel
        {
            ProductTypeId = productType.ProductTypeId,
            ProductTypeName = productType.Name,
            Sizes = allSizes.Select(s => new SizeCheckItem
            {
                SizeId = s.SizeId,
                Name = s.Name,
                DisplayOrder = s.DisplayOrder,
                IsSelected = associatedSizeIds.Contains(s.SizeId)
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageSizes(int id, ProductTypeSizeViewModel model)
    {
        var productType = await _context.ProductTypes.FindAsync(id);

        if (productType == null)
            return NotFound();

        if (model.Sizes == null || !model.Sizes.Any(s => s.IsSelected))
        {
            var allSizes = await _context.Sizes
                .Where(s => s.IsActive)
                .OrderBy(s => s.DisplayOrder)
                .ThenBy(s => s.Name)
                .ToListAsync();

            model.ProductTypeName = productType.Name;
            model.Sizes = allSizes.Select(s => new SizeCheckItem
            {
                SizeId = s.SizeId,
                Name = s.Name,
                DisplayOrder = s.DisplayOrder,
                IsSelected = model.Sizes?.FirstOrDefault(m => m.SizeId == s.SizeId)?.IsSelected ?? false
            }).ToList();

            ModelState.AddModelError(string.Empty, "Debe seleccionar al menos una talla.");
            return View(model);
        }

        var existingRelations = await _context.ProductTypeSizes
            .Where(pts => pts.ProductTypeId == id)
            .ToListAsync();

        _context.ProductTypeSizes.RemoveRange(existingRelations);

        var selectedSizeIds = model.Sizes.Where(s => s.IsSelected).Select(s => s.SizeId);

        foreach (var sizeId in selectedSizeIds)
        {
            _context.ProductTypeSizes.Add(new ProductTypeSize
            {
                ProductTypeId = id,
                SizeId = sizeId
            });
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Tallas actualizadas correctamente.";
        return RedirectToAction(nameof(ManageSizes), new { id });
    }

    private bool ProductTypeExists(int id)
    {
        return _context.ProductTypes.Any(e => e.ProductTypeId == id);
    }
}

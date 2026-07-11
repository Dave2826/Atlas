using Atlas.Data;
using Atlas.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Controllers;

[Authorize]
public class DepartmentController : Controller
{
    private readonly AtlasDbContext _context;

    public DepartmentController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? statusFilter)
    {
        var query = _context.Departments.AsQueryable();

        if (string.IsNullOrEmpty(statusFilter) || statusFilter == "Activos")
            query = query.Where(d => d.IsActive);
        else if (statusFilter == "Inactivos")
            query = query.Where(d => !d.IsActive);

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(d => EF.Functions.ILike(d.Name, $"%{searchTerm}%"));

        query = query.OrderBy(d => d.Name);

        var departments = await query.ToListAsync();

        ViewBag.TotalCount = await _context.Departments.CountAsync();
        ViewBag.ActiveCount = await _context.Departments.CountAsync(d => d.IsActive);
        ViewBag.InactiveCount = await _context.Departments.CountAsync(d => !d.IsActive);
        ViewBag.SearchTerm = searchTerm;
        ViewBag.StatusFilter = statusFilter ?? "Activos";

        return View(departments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (await _context.Departments.AnyAsync(d => EF.Functions.ILike(d.Name, department.Name)))
            ModelState.AddModelError("Name", "Ya existe un departamento con ese nombre.");

        if (ModelState.IsValid)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(department);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (department == null)
            return NotFound();

        return View(department);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return NotFound();

        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.DepartmentId)
            return NotFound();

        if (await _context.Departments.AnyAsync(d => EF.Functions.ILike(d.Name, department.Name) && d.DepartmentId != id))
            ModelState.AddModelError("Name", "Ya existe un departamento con ese nombre.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(department.DepartmentId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(department);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (department == null)
            return NotFound();

        ViewBag.ProductCount = await _context.Products.CountAsync(p => p.DepartmentId == id);

        return View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department != null)
        {
            department.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department != null)
        {
            department.IsActive = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool DepartmentExists(int id)
    {
        return _context.Departments.Any(e => e.DepartmentId == id);
    }
}

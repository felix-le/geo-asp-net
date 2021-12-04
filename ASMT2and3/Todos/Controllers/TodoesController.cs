using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todos.Data;
using Todos.Models;

namespace Todos.Controllers
{
  public class TodoesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public TodoesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Todoes
    public async Task<IActionResult> Index()
    {
      var applicationDbContext = _context.Todos.Include(t => t.TodoList);
      return View(await applicationDbContext.ToListAsync());
    }

    // GET: Todoes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var todo = await _context.Todos
          .Include(t => t.TodoList)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (todo == null)
      {
        return NotFound();
      }

      return View(todo);
    }

    // GET: Todoes/Create
    [Authorize]

    public IActionResult Create()
    {
      ViewData["TodoListModelId"] = new SelectList(_context.TodoListModels, "Id", "Name");
      return View();
    }
    [Authorize]

    // POST: Todoes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,TodoName,TodoListModelId,Deadline,DaysTime")] Todo todo)
    {
      if (ModelState.IsValid)
      {
        _context.Add(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["TodoListModelId"] = new SelectList(_context.TodoListModels, "Id", "Name", todo.TodoListModelId);
      return View(todo);
    }
    [Authorize]

    // GET: Todoes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var todo = await _context.Todos.FindAsync(id);
      if (todo == null)
      {
        return NotFound();
      }
      ViewData["TodoListModelId"] = new SelectList(_context.TodoListModels, "Id", "Name", todo.TodoListModelId);
      return View(todo);
    }
    [Authorize]

    // POST: Todoes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,TodoName,TodoListModelId,Deadline,DaysTime")] Todo todo)
    {
      if (id != todo.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(todo);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!TodoExists(todo.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["TodoListModelId"] = new SelectList(_context.TodoListModels, "Id", "Name", todo.TodoListModelId);
      return View(todo);
    }
    [Authorize]

    // GET: Todoes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var todo = await _context.Todos
          .Include(t => t.TodoList)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (todo == null)
      {
        return NotFound();
      }

      return View(todo);
    }
    [Authorize]

    // POST: Todoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var todo = await _context.Todos.FindAsync(id);
      _context.Todos.Remove(todo);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool TodoExists(int id)
    {
      return _context.Todos.Any(e => e.Id == id);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todos.Data;
using Todos.Models;

namespace Todos.Controllers
{
    public class TodoListModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoListModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TodoListModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoListModels.ToListAsync());
        }

        // GET: TodoListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoListModel == null)
            {
                return NotFound();
            }

            return View(todoListModel);
        }

        // GET: TodoListModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoListModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TodoListModel todoListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoListModel);
        }

        // GET: TodoListModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModels.FindAsync(id);
            if (todoListModel == null)
            {
                return NotFound();
            }
            return View(todoListModel);
        }

        // POST: TodoListModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TodoListModel todoListModel)
        {
            if (id != todoListModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoListModelExists(todoListModel.Id))
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
            return View(todoListModel);
        }

        // GET: TodoListModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoListModel == null)
            {
                return NotFound();
            }

            return View(todoListModel);
        }

        // POST: TodoListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoListModel = await _context.TodoListModels.FindAsync(id);
            _context.TodoListModels.Remove(todoListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoListModelExists(int id)
        {
            return _context.TodoListModels.Any(e => e.Id == id);
        }
    }
}

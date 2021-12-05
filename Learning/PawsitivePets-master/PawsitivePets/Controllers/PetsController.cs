using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PawsitivePets.Data;
using PawsitivePets.Models;

namespace PawsitivePets.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pets.Include(p => p.Category).OrderBy(p => p.Category.Name).ThenBy(p => p.Name);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var pet = await _context.Pets
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return View("404");
            }

            return View("Details", pet);
        }

        private static string UploadPhoto(IFormFile Photo)
        {
            // get temp location of uploaded file
            var filePath = Path.GetTempFileName();

            // create a unique name for the image so we dont overwrite any existing images
            // use the Globally Unique Identifier (GUID) class: bob.jpg => a3987asdf-bob.jpg
            var fileName = Guid.NewGuid() + "-" + Photo.FileName;

            // set destination folder dynamically so it works on any system
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\pets\\" + fileName;

            // execute the file save
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }

            // send the unique uploaded file name back so we can save it to the db along with the pet data
            return fileName;
        }


        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,Name,Colour,Age,CategoryId,Price")] Pet pet, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                // upload Photo if any
                if (Photo != null)
                {
                    // call our upload method and store the unique file name we get back
                    var fileName = UploadPhoto(Photo);
                    pet.Photo = fileName; // give the new pet object the unique file name to save
                }

                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", pet.CategoryId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", pet.CategoryId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,Name,Colour,Age,CategoryId,Price")] Pet pet, IFormFile Photo)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // upload Photo if any
                    if (Photo != null)
                    {
                        // call our upload method and store the unique file name we get back
                        var fileName = UploadPhoto(Photo);
                        pet.Photo = fileName; // give the new pet object the unique file name to save
                    }

                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", pet.CategoryId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}

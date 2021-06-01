using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petfinder.Models;

namespace Petfinder.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetfinderContext _context;

        public PetsController(PetfinderContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string selectedValue, string searchString)
        {
            IEnumerable<Pet> pets = _context.Pets;

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "Latest post date", Value = "PetId", Selected = true};
            SelectListItem item2 = new SelectListItem() { Text = "Oldest post date", Value = "PetIdDesc" };
            SelectListItem item3 = new SelectListItem() { Text = "Name A-Z", Value = "Name" };
            SelectListItem item4 = new SelectListItem() { Text = "Name Z-A", Value = "NameDesc" };
            SelectListItem item5 = new SelectListItem() { Text = "Youngest first", Value = "Age" };
            SelectListItem item6 = new SelectListItem() { Text = "Oldest first", Value = "AgeDesc" };
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);

            if (selectedValue != null)
            {
                items.Where(i => i.Value == selectedValue.ToString()).First().Selected = true;
            }

            ViewBag.SortParams = items;

            if(!String.IsNullOrEmpty(searchString))
            {
                pets = pets.Where(p => p.Name.Contains(searchString)).ToList();
            }
            switch (selectedValue)
            {

                case "PetIdDesc":
                    return View(pets.OrderByDescending(p => p.PetId).ToList());
                case "Name":
                    return View(pets.OrderBy(p => p.Name).ToList());
                case "NameDesc":
                    return View(pets.OrderByDescending(p => p.Name).ToList());
                case "Age":
                    return View(pets.OrderBy(p => p.Age).ToList());
                case "AgeDesc":
                    return View(pets.OrderByDescending(p => p.Age).ToList());
                default:
                    return View(pets.OrderBy(p => p.PetId).ToList());

               
            }
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "Id", "Id");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,Name,Age,Sex,Origins,Type,Description,Size,Difficulty,PhotoUrl,ShelterId")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize]
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
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,Name,Age,Sex,Origins,Type,Description,Size,Difficulty,PhotoUrl,ShelterId")] Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection formCollection)
        {
            Petfinder.Models.Sex sexFilter = (Petfinder.Models.Sex)Convert.ToInt32(formCollection["Sex"]);
            Petfinder.Models.Origins originsFilter = (Petfinder.Models.Origins)Convert.ToInt32(formCollection["Origins"]);
            Petfinder.Models.BreedType breedTypeFilter = (Petfinder.Models.BreedType)Convert.ToInt32(formCollection["BreedType"]);
            Petfinder.Models.Size sizeFilter = (Petfinder.Models.Size)Convert.ToInt32(formCollection["Size"]);
            Petfinder.Models.Difficulty difficultyFilter = (Petfinder.Models.Difficulty)Convert.ToInt32(formCollection["Difficulty"]);
            return View(await _context.Pets
                .Where(p => 
                (p.Sex.Equals(sexFilter) || Convert.ToInt32(formCollection["Sex"]).Equals(9)) &&
                (p.Origins.Equals(originsFilter) || Convert.ToInt32(formCollection["Origins"]).Equals(9)) &&
                (p.BreedType.Equals(breedTypeFilter) || Convert.ToInt32(formCollection["BreedType"]).Equals(9)) &&
                (p.Size.Equals(sizeFilter) || Convert.ToInt32(formCollection["Size"]).Equals(9)) &&
                (p.Difficulty.Equals(difficultyFilter) || Convert.ToInt32(formCollection["Difficulty"]).Equals(9)))
                .ToListAsync());
        }
    }
}

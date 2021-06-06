using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
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


        // GET: Pets
        public IActionResult Index(string searchString)
        {
            PetViewModel petViewModel = new PetViewModel
            {
                Pets = _context.Pets,
                Shelters = _context.Shelters
            };
            ViewData["Name"] = new SelectList(_context.Shelters, "Name", "Name");

            if(!String.IsNullOrEmpty(searchString))
            {
                petViewModel.Pets = petViewModel.Pets.Where(p => p.Name.Contains(searchString)).ToList();
            }

            return View(petViewModel);
            
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
            ViewData["Name"] = new SelectList(_context.Shelters, "Name", "Name");
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
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "ShelterId", "ShelterId", pet.ShelterId);
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
        public IActionResult Index(IFormCollection formCollection)
        {

            PetViewModel petViewModel = new PetViewModel
            {
                Pets = _context.Pets,
                Shelters = _context.Shelters
            };

            Sex sexFilter = (Sex)Convert.ToInt32(formCollection["Sex"]);
            Origins originsFilter = (Origins)Convert.ToInt32(formCollection["Origins"]);
            BreedType breedTypeFilter = (BreedType)Convert.ToInt32(formCollection["BreedType"]);
            Size sizeFilter = (Size)Convert.ToInt32(formCollection["Size"]);
            Difficulty difficultyFilter = (Difficulty)Convert.ToInt32(formCollection["Difficulty"]);
            string orderFilter = formCollection["SortParams"];
           // Shelter shelterFilter = formCollection["Difficulty"];

            ViewData["Name"] = new SelectList(petViewModel.Shelters, "Name", "Name");

            petViewModel.Pets = petViewModel.Pets
                .Where(p =>
                (p.Sex.Equals(sexFilter) || Convert.ToInt32(formCollection["Sex"]).Equals(9)) &&
                (p.Origins.Equals(originsFilter) || Convert.ToInt32(formCollection["Origins"]).Equals(9)) &&
                (p.BreedType.Equals(breedTypeFilter) || Convert.ToInt32(formCollection["BreedType"]).Equals(9)) &&
                (p.Size.Equals(sizeFilter) || Convert.ToInt32(formCollection["Size"]).Equals(9)) &&
                (p.Difficulty.Equals(difficultyFilter) || Convert.ToInt32(formCollection["Difficulty"]).Equals(9)))
                .ToList();

            switch (orderFilter)
            {

                case "PetIdDesc":
                    petViewModel.Pets = petViewModel.Pets.OrderByDescending(p => p.PetId).ToList();
                    return View(petViewModel);
                case "Name":
                    petViewModel.Pets = petViewModel.Pets.OrderBy(p => p.Name).ToList();
                    return View(petViewModel);
                case "NameDesc":
                    petViewModel.Pets = petViewModel.Pets.OrderByDescending(p => p.Name).ToList();
                    return View(petViewModel);
                case "Age":
                    petViewModel.Pets = petViewModel.Pets.OrderBy(p => p.Age).ToList();
                    return View(petViewModel);
                case "AgeDesc":
                    petViewModel.Pets = petViewModel.Pets.OrderByDescending(p => p.Age).ToList();
                    return View(petViewModel);
                default:
                    petViewModel.Pets = petViewModel.Pets.OrderBy(p => p.PetId).ToList();
                    return View(petViewModel);


            }
        }
    }
}

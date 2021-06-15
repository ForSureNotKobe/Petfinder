using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petfinder.Helpers;
using Petfinder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetfinderContext _context;
        private IHostingEnvironment _environment;

        public PetsController(PetfinderContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [AllowAnonymous]
        // GET: Pets
        public IActionResult Index(string searchString)
        {
            PetViewModel petViewModel = new PetViewModel
            {
                Pets = _context.Pets,
                Shelters = _context.Shelters
            };
            ViewData["Name"] = new SelectList(_context.Shelters, "ShelterId", "Name");

            if (!String.IsNullOrEmpty(searchString))
            {
                petViewModel.Pets = petViewModel.Pets.Where(p => p.Name.Contains(searchString)).ToList();
            }

            return View(petViewModel);

        }

        [AllowAnonymous]
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
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (currentUser.ShelterId != null)
            {
                return View();
            }
            else
                return RedirectToAction("Create", "Shelters");

        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,Name,Age,Sex,Origins,BreedType,Description,Size,Difficulty,PhotoUrl")] Pet pet)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (currentUser.ShelterId != null)
            {
                if (ModelState.IsValid)
                {
                    pet.ShelterId = (int)currentUser.ShelterId;
                    _context.Add(pet);
                    await _context.SaveChangesAsync();

                    _context.Shelters.FirstOrDefault(s => s.ShelterId == currentUser.ShelterId).Pets.Add(pet);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(pet);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        // GET: Pets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            if (currentUser.ShelterId != pet.ShelterId)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,Name,Age,Sex,Origins,BreedType,Description,Size,Difficulty,PhotoUrl")] Pet pet)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pet.ShelterId = (int)currentUser.ShelterId;
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
            return View(pet);
        }

        // GET: Pets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

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

            if (currentUser.ShelterId != pet.ShelterId)
            {
                return RedirectToAction(nameof(Index));
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {

            PetViewModel petViewModel = new PetViewModel
            {
                Pets = _context.Pets,
                Shelters = _context.Shelters
            };

            ViewData["Name"] = new SelectList(petViewModel.Shelters, "ShelterId", "Name");

            Sex sexFilter = (Sex)Convert.ToInt32(formCollection["Sex"]);
            Origins originsFilter = (Origins)Convert.ToInt32(formCollection["Origins"]);
            BreedType breedTypeFilter = (BreedType)Convert.ToInt32(formCollection["BreedType"]);
            Size sizeFilter = (Size)Convert.ToInt32(formCollection["Size"]);
            Difficulty difficultyFilter = (Difficulty)Convert.ToInt32(formCollection["Difficulty"]);
            string orderFilter = formCollection["SortParams"];
            int shelterFilter = Convert.ToInt32(formCollection["Shelter"]);



            petViewModel.Pets = petViewModel.Pets
                .Where(p =>
                (p.Sex.Equals(sexFilter) || Convert.ToInt32(formCollection["Sex"]).Equals(9)) &&
                (p.Origins.Equals(originsFilter) || Convert.ToInt32(formCollection["Origins"]).Equals(9)) &&
                (p.BreedType.Equals(breedTypeFilter) || Convert.ToInt32(formCollection["BreedType"]).Equals(9)) &&
                (p.Size.Equals(sizeFilter) || Convert.ToInt32(formCollection["Size"]).Equals(9)) &&
                (p.Difficulty.Equals(difficultyFilter) || Convert.ToInt32(formCollection["Difficulty"]).Equals(9)) &&
                (p.ShelterId.Equals(shelterFilter) || shelterFilter == 0))
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


        [HttpPost("FileUpload")]
        public async Task<IActionResult> FileUpload(List<IFormFile> files)
        {
            string uploads = Path.Combine(_environment.WebRootPath, "/uploads");
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View();
        }
    }
}

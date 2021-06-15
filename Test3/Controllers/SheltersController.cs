using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petfinder.Helpers;
using Petfinder.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Controllers
{
    public class SheltersController : Controller
    {
        private readonly PetfinderContext _context;

        public SheltersController(PetfinderContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Shelters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shelters.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Shelters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters
                .FirstOrDefaultAsync(m => m.ShelterId == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelters/Create
        public IActionResult Create()
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (currentUser.ShelterId != null)
            {
                //POPUP ==> SHELTER ALREADY REGISTERED
                return (RedirectToAction("Index"));
            }

            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShelterId,Name,Email,PhoneNumber,Address,Nip")] Shelter shelter)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            shelter.User = currentUser;
            shelter.UserId = currentUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(shelter);

                await _context.SaveChangesAsync();

                currentUser.Shelter = shelter;
                currentUser.ShelterId = shelter.ShelterId;
                _context.Update(currentUser);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters.FindAsync(id);

            if (shelter == null)
            {
                return NotFound();
            }

            if (UserHelper.GetCurrentUser(HttpContext, _context).Shelter != shelter)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShelterId,Name,Email,PhoneNumber,Address,Nip")] Shelter shelter)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (id != shelter.ShelterId)
            {
                return NotFound();
            }

            if (currentUser.ShelterId != shelter.ShelterId)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shelter.UserId = currentUser.Id;
                    _context.Update(shelter);
                    currentUser.Shelter = shelter;
                    _context.Update(currentUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelterExists(shelter.ShelterId))
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
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters
                .FirstOrDefaultAsync(m => m.ShelterId == id);
            if (shelter == null)
            {
                return NotFound();
            }

            if (shelter.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (currentUser.ShelterId != shelter.ShelterId)
            {
                return RedirectToAction(nameof(Index));
            }

            var petsToDelete = _context.Pets.Where(p => p.ShelterId == shelter.ShelterId).ToList();

            foreach (Pet pet in petsToDelete)
            {
                _context.Pets.Remove(pet);
            }

            _context.Shelters.Remove(shelter);
            currentUser.ShelterId = null;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelters.Any(e => e.ShelterId == id);
        }
    }
}

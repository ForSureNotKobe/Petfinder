using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petfinder.Helpers;
using Petfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Controllers
{
    public class PostsController : Controller
    {
        private readonly PetfinderContext _context;

        public PostsController(PetfinderContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Posts
        public async Task<IActionResult> Index(String selectedValue)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "Latest first", Value = "PostId", Selected = true };
            SelectListItem item2 = new SelectListItem() { Text = "Oldest first", Value = "PostIdDesc" };
            SelectListItem item3 = new SelectListItem() { Text = "Title A-Z", Value = "Title" };
            SelectListItem item4 = new SelectListItem() { Text = "Title Z-A", Value = "TitleDesc" };
            SelectListItem item5 = new SelectListItem() { Text = "Type", Value = "Type" };
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);

            if (selectedValue != null)
            {
                items.Where(i => i.Value == selectedValue.ToString()).First().Selected = true;
            }

            ViewBag.SortParams = items;

            switch (selectedValue)
            {
                case "PostIdDesc":
                    return View(await _context.Posts.OrderByDescending(p => p.PostId).ToListAsync());
                case "Title":
                    return View(await _context.Posts.OrderBy(p => p.Title).ToListAsync());
                case "TitleDesc":
                    return View(await _context.Posts.OrderByDescending(p => p.Title).ToListAsync());
                case "Type":
                    return View(await _context.Posts.OrderByDescending(p => p.Type).ToListAsync());
                default:
                    return View(await _context.Posts.OrderBy(p => p.PostId).ToListAsync());
            }
        }

        [AllowAnonymous]
        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Type,Title,Description,PhotoUrl")] Post post)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            post.User = currentUser;
            post.UserId = currentUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            currentUser.Posts.Add(_context.Posts.Last());

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (post.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Type,Title,Description,PhotoUrl")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            if (ModelState.IsValid)
            {
                try
                {
                    post.UserId = currentUser.Id;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (post.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (post.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}

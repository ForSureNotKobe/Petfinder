﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petfinder.Helpers;
using Petfinder.Models;

namespace Petfinder.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly PetfinderContext _context;
        private readonly UserManager<User> _userManager;

        public ClinicsController(PetfinderContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Clinics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clinics.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Clinics/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Name"] = new SelectList(_context.Ratings, "Opinion", "Opinion");
            ClinicRatingModel clinicRatingModel = new ClinicRatingModel
            {
                Clinics = _context.Clinics,
                Ratings = _context.Ratings
            };
            var clinic = clinicRatingModel.Clinics                
                .FirstOrDefault(m => m.ClinicId == id);
            return View(clinicRatingModel);

        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext,_context);

            if (currentUser.ClinicId != null)
            {
                return(RedirectToAction("Index"));
            }            

            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,Address,Nip")] Clinic clinic)
        {
            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);

            clinic.User = currentUser;
            clinic.UserId = currentUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(clinic);
                await _context.SaveChangesAsync();

                currentUser.Clinic = clinic;
                _context.Update(currentUser);

                return RedirectToAction(nameof(Index));
            }
            currentUser.ClinicId = _context.Clinics.Last().ClinicId;

            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (clinic.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,Address,Nip")] Clinic clinic)
        {
            if (id != clinic.ClinicId)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (clinic.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.ClinicId))
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
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.ClinicId == id);
            if (clinic == null)
            {
                return NotFound();
            }

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (clinic.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            var clinic = await _context.Clinics.FindAsync(id);

            var currentUser = UserHelper.GetCurrentUser(HttpContext, _context);
            if (clinic.UserId != currentUser.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(int id)
        {
            return _context.Clinics.Any(e => e.ClinicId == id);
        }
       
    }
}

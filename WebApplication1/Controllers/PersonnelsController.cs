using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PersonnelsController : Controller
    {
        private ApplicationDbContext _context;

        public PersonnelsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Personnels
        public ActionResult Index(string qryName, string sortName)
        {
            var logged_id = User.Identity.GetUserId();
            var personnels = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .OrderByDescending(p => p.Created_at)
                                .ToList();

            // Search qry
            if(!String.IsNullOrEmpty(qryName))
            {
                personnels = personnels.Where(p => p.Name.Contains(qryName)).ToList();
            }

            // Sort name
            if (!String.IsNullOrEmpty(sortName))
            {
                if(sortName == "asc") {
                    personnels = personnels.OrderBy(p => p.Name).ToList();

                } else if (sortName == "desc") {
                    personnels = personnels.OrderByDescending(p => p.Name).ToList();
                }
            }

            return View(personnels);
        }

        // GET: Personnels/Details/5
        public ActionResult Details(int id)
        {
            var logged_id = User.Identity.GetUserId();
            var personnel = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .SingleOrDefault(p => p.Id == id);
            
            if (personnel == null)
                return HttpNotFound();

            var viewModel = new PersonnelViewModel
            {
                Personnel = personnel,
                Genders = _context.Genders.ToList(),
            };

            return View("Details", viewModel);
        }

        // GET: Personnels/Create
        public ActionResult Create()
        {
            var viewModel = new PersonnelViewModel
            {
                Genders = _context.Genders.ToList()
            };
            
            return View(viewModel);
        }

        // POST: Personnels/Create
        [HttpPost]
        public ActionResult Create(Personnel personnel)
        {
            // Debug error
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                var viewModel = new PersonnelViewModel
                {
                    Personnel = personnel,
                    Genders = _context.Genders.ToList(),
                };

                return View("Create", viewModel);
            }

            // Assign logged in user id to personnel
            personnel.Created_by = User.Identity.GetUserId();

            // TODO: Add insert logic here
            DateTime created_at = DateTime.Now;
            personnel.Created_at = created_at;
            
            _context.Personnels.Add(personnel);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Personnels/Edit/5
        public ActionResult Edit(int id)
        {
            var logged_id = User.Identity.GetUserId();
            var personnel = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .SingleOrDefault(p => p.Id == id);

            if (personnel == null)
                return HttpNotFound();

            var viewModel = new PersonnelViewModel
            {
                Personnel = personnel,
                Genders = _context.Genders.ToList(),
            };

            return View("Edit", viewModel);
        }

        // POST: Personnels/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Personnel personnel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PersonnelViewModel
                {
                    Personnel = personnel,
                    Genders = _context.Genders.ToList(),
                };

                return View("Edit", viewModel);
            }

            var personnelInDb = _context.Personnels.Single(p => p.Id == id);

            personnelInDb.Name = personnel.Name;
            personnelInDb.GenderId = personnel.GenderId;
            personnelInDb.DOB = personnel.DOB;
            personnelInDb.POB = personnel.POB;
            personnelInDb.PhoneNumber = personnel.PhoneNumber;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int id)
        {
            var logged_id = User.Identity.GetUserId();
            var personnel = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .SingleOrDefault(p => p.Id == id);

            if (personnel == null)
                return HttpNotFound();

            _context.Personnels.Remove(personnel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

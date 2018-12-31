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
        public ActionResult Index()
        {
            var personnels = _context.Personnels.ToList();
            
            return View(personnels);
        }

        // GET: Personnels/Details/5
        public ActionResult Details(int id)
        {
            var personnel = _context.Personnels.SingleOrDefault(p => p.Id == id);
            
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
            var genders = _context.Genders.ToList();
            var viewModel = new PersonnelViewModel
            {
                Genders = genders
            };
            
            return View(viewModel);
        }

        // POST: Personnels/Create
        [HttpPost]
        public ActionResult Create(Personnel personnel)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new PersonnelViewModel
                {
                    Personnel = personnel,
                    Genders = _context.Genders.ToList(),
                };

                return View("Create", viewModel);
            }

            // TODO: Add insert logic here
            personnel.Created_by = User.Identity.GetUserId<int>();

            DateTime created_at = DateTime.Now;
            personnel.Created_at = created_at;

            _context.Personnels.Add(personnel);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Personnels/Edit/5
        public ActionResult Edit(int id)
        {
            var personnel = _context.Personnels.SingleOrDefault(p => p.Id == id);

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

            _context.Personnels.Add(personnel);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int id)
        {
            var personnel = _context.Personnels.SingleOrDefault(p => p.Id == id);

            if (personnel == null)
                return HttpNotFound();

            _context.Personnels.Remove(personnel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

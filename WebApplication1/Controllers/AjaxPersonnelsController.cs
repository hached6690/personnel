using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AjaxPersonnelsController : Controller
    {
        private ApplicationDbContext _context;

        public AjaxPersonnelsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: AjaxPersonnels
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            var logged_id = User.Identity.GetUserId();
            var personnels = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .OrderByDescending(p => p.Created_at)
                                .ProjectTo<AjaxPersonnelDto>()
                                .ToList();

            return Json(personnels, JsonRequestBehavior.AllowGet);
        }

        // GET: AjaxPersonnels/Create
        public ActionResult Create()
        {
            var viewModel = new PersonnelViewModel
            {
                Genders = _context.Genders.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Personnel personnel)
        {
            if(!ModelState.IsValid)
            {
                return Json(new { result = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            // Assign logged in user id to personnel
            personnel.Created_by = User.Identity.GetUserId();

            DateTime created_at = DateTime.Now;
            personnel.Created_at = created_at;

            _context.Personnels.Add(personnel);
            _context.SaveChanges();

            return Json(new { result = true, msg = "New personnel is stored successfully." });
        }

        // GET: AjaxPersonnels/Edit/5
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

        [HttpPost]
        public ActionResult Edit(int id, Personnel personnel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var logged_id = User.Identity.GetUserId();
            var personnelInDb = _context.Personnels
                                    .Where(p => p.Created_by == logged_id)
                                    .Single(p => p.Id == id);

            personnelInDb.Name = personnel.Name;
            personnelInDb.GenderId = personnel.GenderId;
            personnelInDb.DOB = personnel.DOB;
            personnelInDb.POB = personnel.POB;
            personnelInDb.PhoneNumber = personnel.PhoneNumber;

            _context.SaveChanges();

            return Json(new { result = true, msg = "This personnel is updated successfully." });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var logged_id = User.Identity.GetUserId();
            var personnel = _context.Personnels
                                .Where(p => p.Created_by == logged_id)
                                .SingleOrDefault(p => p.Id == id);

            if (personnel == null)
            {
                return Json(new { result = false, msg = "Record is not found." });
            }

            _context.Personnels.Remove(personnel);
            _context.SaveChanges();

            return Json(new { result = true, msg = "This personnel is deleted successfully." });
        }
    }
}

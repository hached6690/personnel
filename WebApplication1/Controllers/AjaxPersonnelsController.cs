using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index(string qryName, string sortName)
        {
            return View();
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
    }
}

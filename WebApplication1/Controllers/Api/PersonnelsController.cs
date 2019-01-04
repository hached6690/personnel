using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class PersonnelsController : ApiController
    {
        private ApplicationDbContext _context;

        public PersonnelsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: api/Personnels
        [HttpGet]
        public IEnumerable<PersonnelDto> GetPersonnels()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            var qryDOB = nvc["qryDOB"];

            var personnels = _context.Personnels
                                .OrderByDescending(p => p.Created_at)
                                .ProjectTo<PersonnelDto>()
                                .ToList();

            // Filter by DOB
            if (!String.IsNullOrEmpty(qryDOB))
            {
                personnels = personnels.Where(p => p.DOB < Convert.ToDateTime(qryDOB)).ToList();
            }

            return personnels;
        }

        [HttpGet]
        [Authorize]
        [Route("api/ajax/personnels")]
        public IEnumerable<PersonnelDto> AjaxPersonnels()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            var qryDOB = nvc["qryDOB"];

            var logged_in = User.Identity.GetUserId();
            var personnels = _context.Personnels
                                .Where(p => p.Created_by == logged_in)
                                .OrderByDescending(p => p.Created_at)
                                .ProjectTo<PersonnelDto>()
                                .ToList();

            // Filter by DOB
            if (!String.IsNullOrEmpty(qryDOB))
            {
                personnels = personnels.Where(p => p.DOB < Convert.ToDateTime(qryDOB)).ToList();
            }

            return personnels;
        }

        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        [Route("api/ajax/personnels/create")]
        public IHttpActionResult CreateAjaxPersonnel(Personnel personnel)
        {
            return Json(personnel);

            // Assign logged in user id to personnel
            //personnel.Created_by = User.Identity.GetUserId();

            //DateTime created_at = DateTime.Now;
            //personnel.Created_at = created_at;

            //_context.Personnels.Add(personnel);
            //_context.SaveChanges();

            //return Json(personnel);
        }
    }
}

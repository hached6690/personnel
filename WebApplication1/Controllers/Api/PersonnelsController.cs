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

        // GET: api/Personnels/5
        public PersonnelDto GetPersonnel(int id)
        {
            var personnel = _context.Personnels
                                .ProjectTo<PersonnelDto>()
                                .SingleOrDefault(p => p.Id == id);

            if(personnel == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return personnel;
        }

        // POST: api/personnels
        [HttpPost]
        public Personnel CreatePersonnel(Personnel personnel)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // Assign logged in user id to personnel
            var logged_id = User.Identity.GetUserId();
            personnel.Created_by = logged_id;

            DateTime created_at = DateTime.Now;
            personnel.Created_at = created_at;

            _context.Personnels.Add(personnel);
            _context.SaveChanges();

            return personnel;
        }

        // PUT: api/personnels/5
        [HttpPut]
        public void UpdatePersonnel(int id, Personnel personnel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var personnelInDb = _context.Personnels.SingleOrDefault(p => p.Id == id);

            if(personnelInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            personnelInDb.Name = personnel.Name;
            personnelInDb.GenderId = personnel.GenderId;
            personnelInDb.DOB = personnel.DOB;
            personnelInDb.POB = personnel.POB;
            personnelInDb.PhoneNumber = personnel.PhoneNumber;

            _context.SaveChanges();
        }

        // DELETE: api/personnels/5
        [HttpDelete]
        public void DeletePersonnel(int id)
        {
            var personnelInDb = _context.Personnels.SingleOrDefault(p => p.Id == id);

            if (personnelInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Personnels.Remove(personnelInDb);
            _context.SaveChanges();
        }


        // For ajax request listing, create, update, delete

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
            // Assign logged in user id to personnel
            personnel.Created_by = User.Identity.GetUserId();

            DateTime created_at = DateTime.Now;
            personnel.Created_at = created_at;

            _context.Personnels.Add(personnel);
            _context.SaveChanges();

            return Json(personnel);
        }
    }
}

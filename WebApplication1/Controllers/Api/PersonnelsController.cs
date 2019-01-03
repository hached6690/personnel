using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
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
        public IEnumerable<Personnel> GetPersonnels()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            var qryDOB = nvc["qryDOB"];

            var personnels = _context.Personnels.OrderByDescending(p => p.Created_at).ToList();

            // Filter by DOB
            if (!String.IsNullOrEmpty(qryDOB))
            {
                personnels = personnels.Where(p => p.DOB < Convert.ToDateTime(qryDOB)).ToList();
            }

            return personnels;
        }
    }
}

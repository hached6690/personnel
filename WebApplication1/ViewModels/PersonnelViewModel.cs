using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class PersonnelViewModel
    {
        public IEnumerable<Gender> Genders { get; set; }
        public Personnel Personnel { get; set; }
    }
}
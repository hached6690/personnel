using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Dtos
{
    public class AjaxPersonnelDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public GenderDto Gender { get; set; }

        public string DOB { get; set; }

        [StringLength(256)]
        public string POB { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }
    }
}
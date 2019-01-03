using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Dtos
{
    public class PersonnelDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [StringLength(128)]
        public string User { get; set; }
    }
}
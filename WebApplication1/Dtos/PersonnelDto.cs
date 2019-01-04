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
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DOB { get; set; }

        [StringLength(256)]
        public string POB { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("getutcdate()")]
        public DateTime Created_at { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Personnel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int Gender { get; set; }

        [StringLength(12)]
        public string DOB { get; set; }

        [StringLength(256)]
        public string POB { get; set; }

        [StringLength(12)]
        [Display(Name="Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        public int Created_by { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("getutcdate()")]
        public DateTime Created_at { get; set; }
    }
}
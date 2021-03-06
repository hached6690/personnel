﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Personnel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Display(Name="Gender")]
        public int GenderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [StringLength(256)]
        public string POB { get; set; }

        [StringLength(12)]
        [Display(Name="Phone")]
        public string PhoneNumber { get; set; }

        [StringLength(128)]
        public string Created_by { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("getutcdate()")]
        public DateTime Created_at { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [ForeignKey("Created_by")]
        public virtual ApplicationUser User { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace element_backstage.Models
{
    public class Customers
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public bool Sex { get; set; }
        [Range(minimum: 0, maximum: 100)]
        public int Age { get; set; }
        public DateTime Birth { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public DateTime EidtTime { get; set; }
        public DateTime CreateTime { set; get; }
        public bool Del { get; set; }
    }
}
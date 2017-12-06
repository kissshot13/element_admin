using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace element_backstage.Models
{
    public class Account
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Del { get; set; }
    }
}
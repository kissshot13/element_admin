using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public bool Del { get; set; }
    }
}

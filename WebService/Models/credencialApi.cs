using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class credencialApi
    {
        [Key]
        public int id { get; set; }
        public string  name { get; set; }
        public string passwords { get; set; }
    }
}

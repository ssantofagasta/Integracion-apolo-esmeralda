using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Usuario
    {
        public long ID { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Communes
    {
        [Key]
        public Nullable<long> id { get; set; }
        public string name { get; set; }
        public string code_deis { get; set; }
    }
}

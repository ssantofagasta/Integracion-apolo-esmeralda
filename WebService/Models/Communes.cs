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
        public Nullable<string> name { get; set; }
        public Nullable<string> code_deis { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Communes
    {
        [Key]
        public long? id { get; set; }

        public string name { get; set; }
        public string code_deis { get; set; }
    }
}

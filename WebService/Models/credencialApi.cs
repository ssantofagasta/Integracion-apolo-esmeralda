using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class credencialApi
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string passwords { get; set; }
    }
}

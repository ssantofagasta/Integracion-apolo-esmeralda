using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class users
    {
        [Key]
        public long id { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int run { get; set; }
        public long laboratory_id { get; set; }
    }
}

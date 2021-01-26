using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Establishments
    {
        /// <summary>
        /// Identidad del establecimiento en el monitor esmeralda
        /// </summary>
        [Key]
        public long? id { get; set; }
        public string new_code_deis { get; set; }
    }
}

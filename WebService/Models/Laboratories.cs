using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Laboratories
    {
        /// <summary>
        /// Identidad del laboratorio en el monitor esmeralda
        /// </summary>
        [Key]
        public long? id { get; set; }

        public long? id_openagora { get; set; }
        public bool minsal_ws { get; set; }
        public string token_ws { get; set; }
    }
}

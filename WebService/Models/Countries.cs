using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Countries
    {
        /// <summary>
        /// Identificador interno de los datos de la tabla countries
        /// </summary>
        [Key]
        public long? id { get; set; }

        public int id_minsal { get; set; }

        public string name { get; set; }

    }
}

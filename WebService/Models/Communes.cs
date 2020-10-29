using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    /// <summary>
    /// Información de la comuna
    /// </summary>
    public class Communes
    {
        /// <summary>
        /// Identidad de la comuna en el monitor esmeralda
        /// </summary>
        [Key]
        public long? id { get; set; }

        /// <summary>
        /// Nombre de la comuna
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Código DEIS de la comuna
        /// </summary>
        public string code_deis { get; set; }
    }
}

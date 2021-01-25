using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    /// <summary>
    /// Usuario dentro del monitor esmeralda.
    /// </summary>
    public class users
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        [Key]
        public long id { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Correo del usuario
        /// </summary>
        public string email { get; set; }
        
        /// <summary>
        /// Contraseña encryptada del usuario.
        /// </summary>
        public string password { get; set; }
        
        /// <summary>
        /// RUN del usuario
        /// </summary>
        public int run { get; set; }


        /// <summary>
        /// DV del usuario
        /// </summary>
        public char dv { get; set; }


        /// <summary>
        /// Identidad del laboratorio asignado al usuario.
        /// </summary>
        public long? laboratory_id { get; set; }
    }
}

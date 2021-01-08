using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    /// <summary>
    /// Datos demográficos del paciente.
    /// </summary>
    public class demographics
    {
        /// <summary>
        /// Identificador interno de los datos demográficos
        /// </summary>
        [Key]
        public long? id { get; set; }

        /// <summary>
        /// Tipo de la dirección. Estos pueden ser: Calle, Pasaje, Avenida y Camino
        /// </summary>
        public string street_type { get; set; }
        
        /// <summary>
        /// Dirección de residencia
        /// </summary>
        public string address { get; set; }
        
        /// <summary>
        /// Número de la casa
        /// </summary>
        public string number { get; set; }
        
        /// <summary>
        /// Número de departamento
        /// </summary>
        public string department { get; set; }
        
        /// <summary>
        /// Población
        /// </summary>
        public string suburb { get; set; }
        
        /// <summary>
        /// País
        /// </summary>
        public string nationality { get; set; }
        
        /// <summary>
        /// Identificador de la región de referencia
        /// </summary>
        public long? region_id { get; set; }
        
        /// <summary>
        /// Identificador de la comuna
        /// </summary>
        public long? commune_id { get; set; }
        
        /// <summary>
        /// latitud de la dirección
        /// </summary>
        public double? latitude { get; set; }
        
        /// <summary>
        /// longitud de la dirección
        /// </summary>
        public double? longitude { get; set; }
        
        /// <summary>
        /// Teléfono de contacto del paciente
        /// </summary>
        public string telephone { get; set; }
        
        /// <summary>
        /// Ciudad 
        /// </summary>
        public string city { get; set; }
        
        /// <summary>
        /// Otro teléfono de contacto
        /// </summary>
        public string telephone2 { get; set; }
        
        /// <summary>
        /// Correo electrónico de contacto.
        /// </summary>
        public string email { get; set; }
        
        /// <summary>
        /// Identificación del paciente.
        /// </summary>
        public long? patient_id { get; set; }
        
        /// <summary>
        /// Fecha de creación de los datos demográficos. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? created_at { get; set; }
        
        /// <summary>
        /// Fecha de actualización de los datos demográficos. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? updated_at { get; set; }
        
        /// <summary>
        /// Fecha de eliminación de los datos demográficos. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? deleted_at { get; set; }
    }
}

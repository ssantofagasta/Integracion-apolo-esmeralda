using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    /// <summary>
    /// Datos del paciente
    /// </summary>
    public class Patients
    {
        /// <summary>
        /// Identificador del paciente
        /// </summary>
        [Key]
        public long id { get; set; }

        /// <summary>
        /// RUN del paciente. El run debe ir sin el dígito verificador
        /// </summary>
        public int? run { get; set; }
        
        /// <summary>
        /// Dígito verificador del RUN
        /// </summary>
        public string dv { get; set; }

        /// <summary>
        /// Nombres del paciente
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Otra identificación. En el caso que el paciente no tenga un RUN,
        /// se puede indicar aquí el pasaporte u otro identificador
        /// </summary>
        public string other_identification { get; set; }

        /// <summary>
        /// Apellido Paterno
        /// </summary>
        public string fathers_family { get; set; }
        
        /// <summary>
        /// Apellido Materno
        /// </summary>
        public string mothers_family { get; set; }
        
        /// <summary>
        /// Genero del paciente. Los valores que puede tener son los siguientes:
        /// male, female, other, unknown
        /// </summary>
        public string gender { get; set; }
        
        /// <summary>
        /// Fecha de nacimiento. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? birthday { get; set; }
        
        /// <summary>
        /// Estado del paciente. Los valores que puede tener son:
        /// Nulo, Fallecido, Hospitalizado Medio, Residencia Sanitaria, Ambulatorio y
        /// Hospitalizado UTI
        /// </summary>
        public string status { get; set; }
        
        /// <summary>
        /// Fecha de fallecimiento. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? deceased_at { get; set; }
        
        /// <summary>
        /// Fecha de creación del paciente en sistema. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? created_at { get; set; }
        
        /// <summary>
        /// Fecha de actualización del paciente. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? updated_at { get; set; }
        
        /// <summary>
        /// Fecha de eliminación del paciente. El formato de la fecha debe estar en el estándar ISO 8601
        /// </summary>
        public DateTime? deleted_at { get; set; }
    }
}

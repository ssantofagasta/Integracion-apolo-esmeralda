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
        /// RUN del paciente
        /// </summary>
        /// <remarks>
        /// El run debe ir sin el dígito verificador
        /// </remarks>
        public int? run { get; set; }
        
        /// <summary>
        /// Dígito verificador del RUN
        /// </summary>
        public string dv { get; set; }
        
        /// <summary>
        /// Otra identificación
        /// </summary>
        /// <remarks>
        /// En el caso que el paciente no tenga un RUN, se puede indicar aquí el pasaporte
        /// u otro identificador
        /// </remarks>
        public string other_identification { get; set; }
        
        /// <summary>
        /// Nombres del paciente
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Apellido Paterno
        /// </summary>
        public string fathers_family { get; set; }
        
        /// <summary>
        /// Apellido Materno
        /// </summary>
        public string mothers_family { get; set; }
        
        /// <summary>
        /// Genero del paciente
        /// </summary>
        /// <remarks>
        /// Los valores que puede tener son los siguientes:
        /// male, female, other, unknown
        /// </remarks>
        public string gender { get; set; }
        
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        /// <remarks>
        /// El formato de la fecha debe estar en el estándar ISO 8601
        /// </remarks>
        public DateTime? birthday { get; set; }
        
        /// <summary>
        /// Estado del paciente
        /// </summary>
        /// <remarks>
        /// Los valores que puede tener son:
        /// Nulo, Fallecido, Hospitalizado Medio, Residencia Sanitaria, Ambulatorio, Hospitalizado UTI
        /// </remarks>
        public string status { get; set; }
        
        /// <summary>
        /// Fecha de fallecimiento
        /// </summary>
        /// <remarks>
        /// El formato de la fecha debe estar en el estándar ISO 8601
        /// </remarks>
        public DateTime? deceased_at { get; set; }
        
        /// <summary>
        /// Fecha de creación del paciente en sistema
        /// </summary>
        public DateTime? created_at { get; set; }
        
        /// <summary>
        /// Fecha de actualización del paciente
        /// </summary>
        /// <remarks>
        /// El formato de la fecha debe estar en el estándar ISO 8601
        /// </remarks>
        public DateTime? updated_at { get; set; }
        
        /// <summary>
        /// Fecha de eliminación del paciente
        /// </summary>
        /// <remarks>
        /// El formato de la fecha debe estar en el estándar ISO 8601
        /// </remarks>
        public DateTime? deleted_at { get; set; }
    }
}

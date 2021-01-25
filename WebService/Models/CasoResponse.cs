using System;
using System.ComponentModel.DataAnnotations;
using WebService.Request;

namespace WebService.Models
{
    /// <summary>
    /// Representa el caso de sospecha junto con la información del paciente.
    /// </summary>
    public class CasoResponse
    {
        /// <summary>
        /// El caso de sospecha
        /// </summary>
        public Sospecha caso { get; set; }

        /// <summary>
        /// EL paciente asociado al caso
        /// </summary>
        public Patients paciente { get; set; }

        /// <summary>
        /// Los datos demográficos del paciente
        /// </summary>
        public demographics demografico { get; set; }

    }
}

namespace WebService.Models_HRA
{
    /// <summary>
    /// Estructura de consulta para recuperar un paciente desde el monitor
    /// </summary>
    public class PacienteHRA
    {
        /// <summary>
        /// RUN del paciente sin el dígito verificador
        /// </summary>
        public string run { get; set; }
        
        /// <summary>
        /// Otro identificador, puede ser el pasaporte o el identificador interno.
        /// </summary>
        public string other_Id { get; set; }
    }
}

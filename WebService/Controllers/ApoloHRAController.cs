using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Models;
using WebService.Models_HRA;
using WebService.Request;
using WebService.Services;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApoloHRAController : ControllerBase
    {
        private readonly ILogger<ApoloHRAController> _logger;
        private readonly EsmeraldaContext _db;

        public ApoloHRAController(ILogger<ApoloHRAController> logger, EsmeraldaContext db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        /// Usado para verificar que el servicio responde al llamado.
        /// </summary>
        /// <returns>Devuelve un valor verdadero si el servicio responde</returns>
        [HttpGet]
        [Route("echoping")]
        public IActionResult EchoPing()
        {
            return Ok(true);
        }

        /// <summary>
        /// Recupera el usuario dentro del monitor que es responsable de la
        /// creación del muestra y por ende del caso de sospecha
        /// </summary>
        /// <param name="users">Una estructura que representa al usuario dentro del sistema</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("user")]
        public ActionResult<users> GetUser([FromBody] users users)
        {
            try
            {
                var cred = _db.users.FirstOrDefault(a => a.run == users.run);
                return Ok(cred);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Usuario no encontrado user:{users}", users);
                return BadRequest("Error.... Intente más tarde." + e);
            }
        }

        /// <summary>
        /// Recupera el identificador interno que tiene un paciente
        /// asociado dentro del monitor esmeralda.
        /// </summary>
        /// <remarks>
        /// La busqueda se realiza por el run del paciente o por otro identificador, el cual
        /// puede ser un pasaporte u otra identificación.
        /// </remarks>
        /// <param name="pa">Estructura con el identificador del paciente</param>
        /// <returns>Un numero interno con el identficador interno, en caso de no encontrar devuelve un nulo</returns>
        /// Test:"OK"
        [HttpPost]
        [Authorize]
        [Route("getPatient_ID")]
        public IActionResult GetPatientId([FromBody] PacienteHRA pa)
        {
            try
            {
                Patients p;
                if (string.IsNullOrEmpty(pa.run))
                    p = _db.patients.FirstOrDefault(a => a.other_identification.Equals(pa.other_Id));
                else
                    p = _db.patients.FirstOrDefault(a => a.run.Equals(int.Parse(pa.run)));

                return p != null? Ok(p.id): Ok(null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Paciente no recuperado, paciente:{pa}", pa);
                return BadRequest("Error.... Intente más tarde." + e);
            }
        }

        /// <summary>
        /// Agrega paciente inexistente en el monitor esmeralda
        /// </summary>
        /// <param name="patients">Datos del paciente que serán ingresados</param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Authorize]
        [Route("AddPatients")]
        public IActionResult AddPatients([FromBody] Patients patients)
        {
            try
            {
                _db.patients.Add(patients);
                _db.SaveChanges();

                return Ok(patients.id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Paciente no guasrdado, paciente:{patients}", patients);
                return BadRequest("Error.... Intente más tarde." + " Error:"+ e);
            }
        }

        /// <summary>
        /// Recupera el identificador interno de la comuna dado el código DEIS del MINSAL.
        /// </summary>
        /// <param name="codeIds">Código DEIS del establecimiento.</param>
        /// <returns>Obj Comuna con atributo id y name</returns>
        [HttpPost]
        [Authorize]
        [Route("getComuna")]
        public IActionResult GetComuna([FromBody] string codeIds)
        {
            try
            {
                var c = _db.communes.Where(x => x.code_deis.Equals(codeIds))
                           .Select(per => new {per.id, per.name, per.code_deis});
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Comuna con error, comuna id:{code_ids}", codeIds);
                return BadRequest("Error.... Intente más tarde.");
            }
        }

        /// <summary>
        /// Agrega los datos demográficos del paciente, tales como la dirección, comuna, teléfono, etc.
        /// </summary>
        /// <param name="demographics">Datos demográficos del paciente</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("AddDemograph")]
        public IActionResult AddDemograph([FromBody] demographics demographics)
        {
            try
            {
                _db.demographics.Add(demographics);
                _db.SaveChanges();
                return Ok("Se Guardo Correctamente la Demografía");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Demografico no agregado, demographics:{demographics}", demographics);
                return BadRequest("Error.....Intente más Tarde"+ e ) ;
            }        
        }

        /// <summary>
        /// Agrega una nueva sospecha de COVID al monitor esmeralda
        /// </summary>
        /// <param name="sospecha">Información que es necesaria para la creación de la sospecha</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("addSospecha")]
        public IActionResult AddSospecha([FromBody] Sospecha sospecha)
        {
            try
            {
                var suspectCase = new SuspectCase
                {
                    age = sospecha.age,
                    gender = sospecha.gender,
                    sample_at = sospecha.sample_at,
                    epidemiological_week = sospecha.epidemiological_week,
                    run_medic = sospecha.run_medic,
                    symptoms = sospecha.symptoms == "Si",
                    pcr_sars_cov_2 = sospecha.pscr_sars_cov_2,
                    sample_type = sospecha.sample_type,
                    epivigila = sospecha.epivigila,
                    gestation = sospecha.gestation,
                    gestation_week = sospecha.gestation_week,
                    close_contact = sospecha.close_contact,
                    functionary = sospecha.functionary,
                    patient_id = sospecha.patient_id,
                    establishment_id = sospecha.establishment_id,
                    user_id = sospecha.user_id,
                    created_at = sospecha.created_at,
                    updated_at = sospecha.updated_at
                };

                _db.suspect_cases.Add(suspectCase);
                _db.SaveChanges();
                return Ok(suspectCase.id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Sospecha no agregada, sospecha:{sospecha}", sospecha);
                return BadRequest("No se guardo correctamente...." + e);
            }
        }

        /// <summary>
        /// Informa la recepción de la muestra por parte del laboratorio de
        /// la muestra que está asociada a la sospecha
        /// </summary>
        /// <remarks>
        /// Los valores necesarios que se debe informar son:
        ///   - id: Identificador de la sospecha
        ///   - reception_at: fecha y hora que se recibió la sospecha en formato estándar ISO 8601 
        ///   - receptor_id: identificador del usuario que recepcionó la muestra, esté usuario debe estar registrado en
        ///   el monitor esmeralda.
        ///   - laboratory_id: Identificador del laboratorio que recepcionó la muestra.
        /// </remarks>
        /// <param name="sospecha">Datos de la recepción de la muestra</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("recepcionMuestra")]
        public IActionResult UdpateSospecha([FromBody] Sospecha sospecha)
        {
            try
            {
                var sospechaActualizada = _db.suspect_cases.Find(sospecha.id);

                if (sospechaActualizada == null) return BadRequest("No se guardo correctamente....");

                sospechaActualizada.reception_at = sospecha.reception_at;
                sospechaActualizada.receptor_id = sospecha.receptor_id;
                sospechaActualizada.laboratory_id = sospecha.laboratory_id;
                sospechaActualizada.updated_at = sospecha.updated_at;

                _db.SaveChanges();

                return Ok("Se Guardo correctamente...");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Sospecha no actualizada, sospeche:{sospecha}", sospecha);
                return BadRequest("No se guardo correctamente....");
            }
        }

        /// <summary>
        /// Informa la entrega del resultado de la muestra al caso de sospecha
        /// </summary>
        /// <remarks>
        /// Los valores necesarios que se debe informar son:
        ///   - id: identificador de la sospecha
        ///   - pscr_sars_cov_2_at: fecha hora en que se obtuvo el resultado en formato estándar ISO 8601
        ///   - pscr_sars_cov_2: resultado de la muestra.
        ///   - validator_id: identificador del responsable en la validación de la muestra, usuario que debe estar registrado en el monitor esmeralda.
        ///   - update_at: fecha de actualización del caso de sospecha en formato estándar ISO 8601
        /// </remarks>
        /// <param name="sospecha">Datos de la entrega del resultado</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Authorize]
        [Route("resultado")]
        public IActionResult UdpateResultado([FromBody] Sospecha sospecha)
        {
            try
            {
                var sospechaActualizada = _db.suspect_cases.Find(sospecha.id);

                if (sospechaActualizada == null) return NotFound(sospecha);

                sospechaActualizada.pcr_sars_cov_2_at = sospecha.pscr_sars_cov_2_at;
                sospechaActualizada.pcr_sars_cov_2 = sospecha.pscr_sars_cov_2;
                sospechaActualizada.validator_id = sospecha.validator_id;
                sospechaActualizada.updated_at = sospecha.updated_at;
                
                _db.SaveChanges();
                return Ok("Exito... se actualizo los resultado..");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Resultado no actualizado, sospecha:{sospecha}", sospecha);
                return BadRequest("No se guardo correctamente....");
            }
        }

        /// <summary>
        /// Recupera el paciente dado un run u otro identificador
        /// </summary>
        /// <param name="buscador">RUN u otro identificador</param>
        /// <returns>Paciente</returns>
        [HttpGet]
        [Authorize]
        [Route("getPatients")]
        public IActionResult GetPatients([FromBody] string buscador) 
        {
            try
            {
                var paciente = RecuperarPaciente(buscador);
                return Ok(paciente);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No se puede recuperar paciente:{buscador}", buscador);
                return BadRequest("No se Encontro Paciente.... problema" + e);
            }     
        }

        /// <summary>
        /// Recupera los casos de sospecha que tiene asociado un paciente de acuerdo
        /// a su RUN u otro documento de identificación como pasaporte.
        /// </summary>
        /// <param name="buscador">RUN o DNI del paciente a consultar</param>
        /// <returns>Un listado con los casos de sospecha que el paciente tiene</returns>
        [HttpGet]
        [Authorize]
        [Route("getSospecha")]
        public IActionResult GetSospeha([FromBody] string buscador)
        {
            try
            { 
                var paciente = RecuperarPaciente(buscador);
                var sospecha = _db.suspect_cases.Where(c => c.patient_id.Equals(paciente.id))
                                  .Select(
                                       s => new Sospecha
                                       {
                                           id = s.id,
                                           age = s.age,
                                           gender = s.gender,
                                           sample_at = s.sample_at,
                                           epidemiological_week = s.epidemiological_week,
                                           run_medic = s.run_medic,
                                           symptoms = s.symptoms.HasValue?s.symptoms.Value? "Si": "No":"No",
                                           pscr_sars_cov_2 = s.pcr_sars_cov_2,
                                           pscr_sars_cov_2_at = s.pcr_sars_cov_2_at,
                                           sample_type = s.sample_type,
                                           epivigila = s.epivigila,
                                           gestation = s.gestation,
                                           gestation_week = s.gestation_week,
                                           close_contact = s.close_contact,
                                           functionary = s.functionary,
                                           patient_id = s.patient_id,
                                           establishment_id = s.establishment_id,
                                           user_id = s.user_id,
                                           created_at = s.created_at,
                                           updated_at = s.updated_at,
                                           symptoms_at = s.symptoms_at,
                                           observation = s.observation
                                       }
                                   ).ToList();
                return Ok(sospecha);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No se pudo recuperar sospecha del paciente:{buscador}", buscador);
                return BadRequest("No se Encontro sospecha.... problema" + e);
            }
        }

        /// <summary>
        /// Recupera los datos demográficos del paciente
        /// </summary>
        /// <param name="buscador">RUN u otro identificador del paciente</param>
        /// <returns>Paciente</returns>
        [HttpGet]
        [Authorize]
        [Route("getDemograph")]
        public IActionResult GetDemograph([FromBody] string buscador)
        {
            try
            {   
                var paciente = RecuperarPaciente(buscador);
                var demographic = _db.demographics.Where(c => c.patient_id.Equals(paciente.id));
                return Ok(demographic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No se pudo recuperar demografico del paciente:{buscador}", buscador);
                return BadRequest("No se Encontro sospecha.... problema" + e);
            }
        }

        private Patients RecuperarPaciente(string buscador)
        {
            var run = int.Parse(buscador);
            var paciente = _db.patients.FirstOrDefault(c => c.run.Equals(run));
            if (paciente == null)
            {
                paciente = _db.patients.FirstOrDefault(c => c.other_identification.Equals(buscador));
            }

            return paciente;
        }
        /// <summary>
        /// Recupera el caso de sospecha dado el identificador del mismo
        /// </summary>
        /// <param name="idCase">Identificador del caso</param>
        [HttpPost]
        [Authorize]
        [Route("getSuspectCase")]
        public IActionResult GetSuspectCase([FromBody] long idCase)
        {
            try
            {
                var caso = _db.suspect_cases.FirstOrDefault(x => x.id == idCase);
                if (caso == null)
                {
                    return BadRequest("No existe el caso");
                }
                var patient = _db.patients.FirstOrDefault(x => x.id == caso.patient_id);
                if (patient == null)
                {
                    return BadRequest("No existe el paciente");
                }
                var demographic = _db.demographics.FirstOrDefault(x => x.patient_id == patient.id);
                if (demographic == null)
                {
                    return BadRequest("No existe el demografico");
                }
                object retorno = new
                {
                    caso = new Sospecha
                    {
                        id = caso.id,
                        sample_at = caso.sample_at,
                        run_medic = caso.run_medic,
                        symptoms = caso.symptoms.HasValue?caso.symptoms.Value? "Si": "No":"No",
                        symptoms_at = caso.symptoms_at,
                        sample_type = caso.sample_type,
                        epivigila = caso.epivigila,
                        gestation = caso.gestation,
                        gestation_week = caso.gestation_week,
                        observation = caso.observation
                    },
                    paciente = patient,
                    demografico = demographic
                };

                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest("Computer system error." + e);
            }
        }
    }
}

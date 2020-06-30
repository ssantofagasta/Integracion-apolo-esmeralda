using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Models;
using WebService.Models_HRA;
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// Test:"OK"
        [HttpGet]
        [Route("echoping")]
        public IActionResult EchoPing()
        {
            return Ok(true);
        }

        /// <summary>
        /// Se Obtiene los datos del usuario logeado.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("user")]
        public IActionResult getUser([FromBody] users users)
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
        /// Metodo de recuperar la ID de bd EME
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>patient_id</returns>
        /// Test:"OK"
        [HttpPost]
        [Authorize]
        [Route("getPatient_ID")]
        public IActionResult getPatient_ID([FromBody] PacienteHRA pa)
        {
            try
            {
                Patients p;
                if (string.IsNullOrEmpty(pa.run))
                    p = _db.patients.FirstOrDefault(a => a.other_identification.Equals(pa.other_Id));
                else
                    p = _db.patients.FirstOrDefault(a => a.run.Equals(int.Parse(pa.run)));

                if (p != null)
                    return Ok(p.id);

                return Ok(null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Paciente no recuperado, paciente:{pa}", pa);
                return BadRequest("Error.... Intente más tarde." + e);
            }
        }

        /// <summary>
        /// Metodo para agregar un paciente.
        /// </summary>
        /// <param name="patients"></param>
        /// <returns></returns>
        /// Test ="OK"
        [HttpPost]
        [Authorize]
        [Route("AddPatients")]
        public IActionResult addPatients([FromBody] Patients patients)
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
        /// Método de obtener getComuna 
        /// </summary>
        /// <param name="code_ids"></param>
        /// <returns>Obj Comuna con atributo id y name</returns>
        /// Teste = "OK"
        [HttpPost]
        [Authorize]
        [Route("getComuna")]
        public IActionResult getComuna([FromBody] string code_ids)
        {
            try
            {
                var c = _db.communes.Where(x => x.code_deis.Equals(code_ids))
                           .Select(per => new {per.id, per.name, per.code_deis});
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Comuna con error, comuna id:{code_ids}", code_ids);
                return BadRequest("Error.... Intente más tarde.");
            }
        }

        /// <summary>
        /// Metodo de Agregar una demografia
        /// </summary>
        /// <param name="demographics"></param>
        /// <returns></returns>
        /// Test = OK
        [HttpPost]
        [Authorize]
        [Route("AddDemograph")]
        public IActionResult addDemograph([FromBody] demographics demographics)
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
        /// Método donde se agrega .
        /// </summary>
        /// <param name="sospecha">json de la sospecha</param>
        /// <returns></returns>
        /// TESTE = OK 
        [HttpPost]
        [Authorize]
        [Route("addSospecha")]
        public IActionResult addSospecha([FromBody] Sospecha sospecha)
        {
            try
            {
                _db.suspect_cases.Add(sospecha);
                _db.SaveChanges();
                return Ok(sospecha.id);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Sospecha no agregada, sospecha:{sospecha}", sospecha);
                return BadRequest("No se guardo correctamente...." + e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sospecha">
        /// ID
        /// reception_at
        /// receptor_id
        /// laboratory_id
        /// </param>
        /// <returns></returns>
        /// Test = OK
        [HttpPost]
        [Authorize]
        [Route("recepcionMuestra")]
        public IActionResult udpateSospecha([FromBody] Sospecha sospecha)
        {
            try
            {
                var sospechaActualizada = _db.suspect_cases.Find(sospecha.id);

                if (sospechaActualizada == null) return BadRequest("No se guardo correctamente....");

                sospechaActualizada.reception_at = sospechaActualizada.reception_at;
                sospechaActualizada.receptor_id = sospechaActualizada.receptor_id;
                sospechaActualizada.laboratory_id = sospechaActualizada.laboratory_id;
                sospechaActualizada.updated_at = sospechaActualizada.updated_at;

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
        /// Update 
        /// </summary>
        /// <param name="sospecha">
        ///     pscr_sars_cov_2_at
        ///     pscr_sars_cov_2
        ///     validator_id
        ///     updated_at
        /// </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Authorize]
        [Route("resultado")]
        public IActionResult udpateResultado([FromBody] Sospecha sospecha)
        {
            try
            {
                var sospechaActualizada = _db.suspect_cases.Find(sospecha.id);

                if (sospechaActualizada == null) return NotFound(sospecha);

                sospechaActualizada.pscr_sars_cov_2_at = sospecha.pscr_sars_cov_2_at;
                sospechaActualizada.pscr_sars_cov_2 = sospecha.pscr_sars_cov_2;
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
        /// Obtener el paciente con una ID 
        /// </summary>
        /// <param name="buscador"></param>
        /// <returns>Paciente</returns>
        /// TEST = OK
        [HttpGet]
        [Authorize]
        [Route("getPatients")]
        public IActionResult getPatients([FromBody] string buscador) 
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
        /// Obtener el sospechas por el rut o other del paciente 
        /// </summary>
        /// <param name="buscador">RUN o DNI del paciente a consultar</param>
        /// <returns>Paciente</returns>
        /// TEST = OK
        [HttpGet]
        [Authorize]
        [Route("getSospecha")]
        public IActionResult getSospeha([FromBody] string buscador)
        {
            try
            { 
                var paciente = RecuperarPaciente(buscador);
                var sospecha = _db.suspect_cases.Where(c => c.patient_id.Equals(paciente.id));
                return Ok(sospecha);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "No se pudo recuperar sospecha del paciente:{buscador}", buscador);
                return BadRequest("No se Encontro sospecha.... problema" + e);
            }
        }

        /// <summary>
        /// Obtener el Demograph por el rut o other del paciente 
        /// </summary>
        /// <param name="buscador"></param>
        /// <returns>Paciente</returns>
        /// TEST = OK
        [HttpGet]
        [Authorize]
        [Route("getDemograph")]
        public IActionResult getDemograph([FromBody] string buscador)
        {
            try
            {   
                var paciente = RecuperarPaciente(buscador);
                var demographic = _db.Demographics.Where(c => c.patient_id.Equals(paciente.id));
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
        /// <summary>
        /// Obtener el sospechas por el rut o other del paciente 
        /// </summary>
        /// <param name="sospecha">
        /// </param>
        /// <returns>Paciente</returns>
        /// TEST = OK
        [HttpGet]
        [Authorize]
        [Route("getSuspectCase")]
        public IActionResult getSuspectCase([FromBody] long idCase)
        {
            try
            {
                var _case = _db.suspect_cases.FirstOrDefault(x => x.id == idCase);
                if (_case == null)
                {
                    return BadRequest("No existe el caso");
                }
                var _patient = _db.patients.FirstOrDefault(x => x.id == _case.patient_id);
                if (_patient == null)
                {
                    return BadRequest("No existe el paciente");
                }
                var _demographic = _db.Demographics.FirstOrDefault(x => x.patient_id == _patient.id);
                if (_demographic == null)
                {
                    return BadRequest("No existe el demografico");
                }
                object retorno = new
                {
                    caso = _case,
                    paciente = _patient,
                    demografico = _demographic
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

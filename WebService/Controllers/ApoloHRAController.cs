using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebService.Services;
using WebService.Models;
using WebService.Helpers;
using Microsoft.Extensions.Configuration;



namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoloHRAController : ControllerBase
    {
        IConfiguration _configuration;
        private LogicToken logicToken; 

        public ApoloHRAController(IConfiguration configuration)
        {
            _configuration = configuration;
            logicToken= new LogicToken(_configuration);
               
           
        }
        [HttpGet]
        [Authorize]
        [Route("echoping")]
        public IActionResult EchoPing()
        {
            return Ok(true);
        }


        [HttpPost]
        [Route("token")]
        public IActionResult Login([FromBody] Usuario model)
        {
            //Llamado db de las credencials.. posibles metodos para guardar las credenciales.
            //EsmeraldaContext context = HttpContext.RequestServices.GetService(
            //                           typeof(WebService.Services.EsmeraldaContext)) 
            //                           as EsmeraldaContext;

            ActionResult response = Unauthorized();
            Usuario usuario = model;
            var _user = logicToken.AuthenticateUsuario(usuario);
            if (User != null)
            {
                var token = logicToken.GenerateToken(usuario);
                response = Ok(new { token = token }); ;
            }
            //response = context.AddSospecha();
            return response;
        }



        
    }
}

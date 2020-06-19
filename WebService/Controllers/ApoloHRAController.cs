using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebService.Services;
using WebService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoloHRAController : ControllerBase
    {

        IConfiguration _configuration;

        public ApoloHRAController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("echoping")]
        public IActionResult EchoPing()
        {
            return Ok(true);
        }


        [HttpPost]
        [Route("token")]
        public IActionResult Login()
        {
            //Llamado db
            //EsmeraldaContext context = HttpContext.RequestServices.GetService(
            //                           typeof(WebService.Services.EsmeraldaContext)) 
            //                           as EsmeraldaContext;

            ActionResult response = Unauthorized();
            Usuario usuario = new Usuario();
            usuario.ID = 1;
            usuario.name="Teste";
            usuario.email = "xx@xx.cl";
            var _user = AuthenticateUsuario(usuario);
            if (User != null)
            {
                var token = GenerateToken(usuario);
                response = Ok(new { token = token }); ;
            }
            //response = context.AddSospecha();
            return response;
        }



        /// <summary>
        /// Crear el token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerateToken(Usuario model)
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, model.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, model.email),
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(24)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token).ToString();
        }

        /// <summary>
        /// Metodo por el cual se aprueba las credenciales
        /// </summary>
        /// <returns></returns>
        /// 
        private Usuario AuthenticateUsuario(Usuario Model)
        {
            Usuario user = new Usuario();
            //Buscar en la Base de dato las credenciales, en este caso se usara una sola cuenta.
            if (Model.email == "xx@xx.cl")
            {
                user.ID = Model.ID;
                user.name = Model.name;
                user.email = Model.email;
            }
            else
            {
                user = null;
            }

            return user;
        }
    }
}

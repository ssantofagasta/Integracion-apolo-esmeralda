using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebService.Helpers
{
    public class LogicToken
    {
        IConfiguration _configuration;

        public LogicToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Crear el token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GenerateToken(Usuario model)
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
                    // Exipra a la 30 minutos.
                    expires: DateTime.UtcNow.AddMinutes(30)
                ) ;

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
        public Usuario AuthenticateUsuario(Usuario Model)
        {
            Usuario user = new Usuario();
            //Buscar en la Base de dato las credenciales, en este caso se usara una sola cuenta.
            if (Model.email.Equals("xx@xx.cl"))
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

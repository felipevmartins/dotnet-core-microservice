using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Identity.Api.Infra;
using Identity.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserContext _userContext;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;


        public AuthController(UserContext userContext, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _userContext = userContext;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }
        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody] User user)
        {
            bool credenciaisValidas = true;
            if (user != null && !String.IsNullOrWhiteSpace(user.email))
            {
                var userBase = _userContext.users.Where(b => b.name == user.name).Select((c) => new
                {
                    name = c.name,
                    password = c.password
                }).FirstOrDefault();

                credenciaisValidas = (userBase != null &&
                    user.name == userBase.name &&
                    user.password == userBase.password);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.name, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.name)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}

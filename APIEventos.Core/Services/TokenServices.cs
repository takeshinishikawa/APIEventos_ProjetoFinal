using APIEventos.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIEventos.Core.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string role)
        {
            //Chave secreta para validação do Token
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("key"));
            var issuer = _configuration.GetValue<string>("Issuer");
            var audience = _configuration.GetValue<string>("Audience");

            //Corpo do JWT
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer, //Adicionando informação do issuer (quem gera o token)
                Audience = audience, //Adicionando informação do audience (quem recebe/utiliza o token)
                Expires = DateTime.UtcNow.AddHours(2), //Quanto tempo vai expirar o token
                Subject = new ClaimsIdentity(new Claim[] //Claims do usuario
                {
                    new Claim(ClaimTypes.Role, role), //Claim de role padrão
                }),
                SigningCredentials = new SigningCredentials( //Adicionando tipo de credencial
                    new SymmetricSecurityKey(key),           //Adicionando chave de validação do token
                    SecurityAlgorithms.HmacSha256Signature)  //Adicionando algoritmo de segurança do token
            };

            // Classe para manipular e gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Criando a estrutura do token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serializa o token, transforma o token criado, criptografa
            return tokenHandler.WriteToken(token);
        }
    }
}

using Globaltec.Dominio.Constantes;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Globaltec.Dominio.Autenticacao
{
    public static class GeradorDeToken
    {
        /// <summary>
        /// Cria um token de autenticação utilizando a chave de segurança como base.
        /// </summary>
        /// <param name="usuario">Usuário.</param>
        /// <returns>Token de autenticação. Bearer Token.</returns>
        public static string GerarTokenDeAutenticacao(string usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveParaGeracaoDeTokenDeAutenticacao = Encoding.ASCII.GetBytes(Chaves.ChaveParaGeracaoDeTokenDeAutenticacao);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveParaGeracaoDeTokenDeAutenticacao), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
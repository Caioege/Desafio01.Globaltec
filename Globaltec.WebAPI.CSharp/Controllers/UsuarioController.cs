using Globaltec.Dominio.Modelos;
using Globaltec.Servicos.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globaltec.WebAPI.CSharp.Controllers
{
    /// <summary>
    /// Rotas relacionadas as ações do <see cref="Usuario"/>.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/Autenticacao")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="usuarioServico"></param>
        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        /// <summary>
        /// Verifica se existe um usuário cadastrado com o acesso fornecido e, se sim, retorna seus dados, juntamente com um token de autenticação.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para acesso.</param>
        /// <returns>Dados do usuário encontrado.</returns>
        [HttpPost("AutentiqueUsuario")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AutentiqueUsuario([FromBody] Credenciais credenciais)
        {
            var respostaParaRequisicao = _usuarioServico.ConsulteUsuarioPorLoginESenha(credenciais);
            return StatusCode(respostaParaRequisicao.CodigoHTTP, respostaParaRequisicao.Resultado);
        }

        /// <summary>
        /// Recebe as credenciais informadas e cadastra um novo usuário com o acesso informado.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para acesso.</param>
        /// <returns>Dados do usuário cadastrado.</returns>
        [HttpPost("GravarNovoUsuario")]
        [ProducesResponseType(typeof(Usuario), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult GravarNovoUsuario([FromBody] Credenciais credenciais)
        {
            var respostaParaRequisicao = _usuarioServico.GraveUsuario(credenciais);
            return StatusCode(respostaParaRequisicao.CodigoHTTP, respostaParaRequisicao.Resultado);
        }
    }
}

using Globaltec.Dominio.Modelos;
using Globaltec.Servicos.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globaltec.WebAPI.CSharp.Controllers
{
    /// <summary>
    /// Rotas relacionadas as ações da <see cref="Pessoa"/>.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/Pessoa")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public class PessoaController : Controller
    {
        private readonly IPessoaServico _pessoaServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="pessoaServico"></param>
        public PessoaController(IPessoaServico pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        /// <summary>
        /// Consulta uma pessoa com o código informado.
        /// </summary>
        /// <param name="codigo">Código da pessoa.</param>
        /// <returns>A pessoa no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ConsultePessoaPorCodigo/{codigo}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorCodigo([FromRoute] int codigo)
        {
            var respostaDaRequisicao = _pessoaServico.ConsultePessoaPorCodigo(codigo);
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }

        /// <summary>
        /// Consulta uma lista de pessoas que residem na UF informada.
        /// </summary>
        /// <param name="uf">UF da pessoa.</param>
        /// <returns>A lista de pessoas no cache para a UF informada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ConsultePessoaPorUF/{uf}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorUF([FromRoute] string uf)
        {
            var respostaDaRequisicao = _pessoaServico.ConsultePessoasPorUF(uf);
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }

        /// <summary>
        /// Consulta a lista de pessoas.
        /// </summary>
        /// <returns>A lista de pessoas no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ConsulteTodasAsPessoas")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsulteTodasAsPessoas()
        {
            var respostaDaRequisicao = _pessoaServico.ConsulteTodasAsPessoas();
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }

        /// <summary>
        /// Grava o cadastro da pessoa informada no cache.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser cadastrada.</param>
        /// <returns>A pessoa que foi gravada no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPost("/GravePessoa")]
        [ProducesResponseType(typeof(Pessoa), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult GravePessoa([FromBody] Pessoa pessoa)
        {
            var respostaDaRequisicao = _pessoaServico.GravePessoa(pessoa);
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }

        /// <summary>
        /// Atualiza o cadastro de uma pessoa, tendo seu código como base.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa para ser atualizada.</param>
        /// <returns>Os dados da pessoa atualizada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPut("/AtualizarPessoa")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AtualizarPessoa([FromBody] Pessoa pessoa)
        {
            var respostaDaRequisicao = _pessoaServico.AtualizarPessoa(pessoa);
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }

        /// <summary>
        /// Remove o cadastro de uma pessoa do cache.
        /// </summary>
        /// <param name="codigo">Código da pessoa a ser removida do cache.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpDelete("/RemoverPessoa/{codigo}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult RemoverPessoa([FromRoute] int codigo)
        {
            var respostaDaRequisicao = _pessoaServico.ExcluirPessoa(codigo);
            return StatusCode(respostaDaRequisicao.CodigoHTTP, respostaDaRequisicao.Resultado);
        }
    }
}
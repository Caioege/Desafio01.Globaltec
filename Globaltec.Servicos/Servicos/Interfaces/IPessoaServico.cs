using Globaltec.Dominio.Modelos;

namespace Globaltec.Servicos.Servicos.Interfaces
{
    public interface IPessoaServico
    {
        /// <summary>
        /// Consulta os dados de uma pessoa pelo seu código.
        /// </summary>
        /// <param name="codigoPessoa">Código para consulta.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> (se existir).</returns>
        RespostaDeRequisicao ConsultePessoaPorCodigo(int codigoPessoa);

        /// <summary>
        /// Consulta os dados das pessoas através da UF fornecida.
        /// </summary>
        /// <param name="uf">UF para consulta. Ex: GO e TO.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        RespostaDeRequisicao ConsultePessoasPorUF(string uf);

        /// <summary>
        /// Consulta todos os cadastros de pessoas.
        /// </summary>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        RespostaDeRequisicao ConsulteTodasAsPessoas();

        /// <summary>
        /// Grava os dados de uma nova pessoa.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser persistida.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> informada.</returns>
        RespostaDeRequisicao GravePessoa(Pessoa pessoa);

        /// <summary>
        /// Atualiza o cadastro de uma pessoa.
        /// </summary>
        /// <param name="pessoa">Pessoa a ser atualizada.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> atualizada.</returns>
        RespostaDeRequisicao AtualizarPessoa(Pessoa pessoa);
        
        /// <summary>
        /// Exclui os dados da pessoa informada.
        /// </summary>
        /// <param name="codigoPessoa">Código da pessoa a ser removida.</param>
        /// <returns>O código HTTP para a requisição, juntamente com uma mensagem informativa da ação.</returns>
        RespostaDeRequisicao ExcluirPessoa(int codigoPessoa);
    }
}
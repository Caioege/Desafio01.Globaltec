using Globaltec.Dominio.Constantes;
using Globaltec.Dominio.Modelos;
using Globaltec.Servicos.Funcoes;
using Globaltec.Servicos.Servicos.Interfaces;
using Globaltec.Servicos.Validacoes;
using System.Net;

namespace Globaltec.Servicos.Servicos
{
    public class PessoaServico : IPessoaServico
    {
        /// <summary>
        /// Lista de pessoas salvas na memória da aplicação, utilizada somente para fins de teste.
        /// </summary>
        public static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(1, "CAIO HENRICK DE OLIVEIRA", "193.133.030-19", "GO", new DateTime(2000, 08, 17)),
            new Pessoa(2, "WALMER ANTÔNIO SOUZA DOS SANTOS", "078.224.520-02", "GO", new DateTime(2000, 08, 17))
        };

        public RespostaDeRequisicao ConsultePessoaPorCodigo(int codigoPessoa)
        {
			try
			{
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == codigoPessoa);
                if (pessoaPersistida == null)
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.RegistroNaoEncontrado);

                return new RespostaDeRequisicao(HttpStatusCode.OK, pessoaPersistida);
			}
            catch (Exception) 
            { 
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor);
            }
        }

        public RespostaDeRequisicao ConsultePessoasPorUF(string uf)
        {
            try
            {
                var pessoas = Pessoas.Where(c => c.UF.ToUpper().Equals(uf.Trim().ToUpper()));
                if (pessoas == null || !pessoas.Any())
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.RegistroNaoEncontrado);

                return new RespostaDeRequisicao(HttpStatusCode.OK, pessoas);
            }
            catch (Exception) 
            {
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }

        public RespostaDeRequisicao ConsulteTodasAsPessoas()
        {
            try
            {
                if (Pessoas == null || !Pessoas.Any())
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.RegistroNaoEncontrado);

                return new RespostaDeRequisicao (HttpStatusCode.OK, Pessoas);
            }
            catch (Exception) 
            { 
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }

        public RespostaDeRequisicao GravePessoa(Pessoa pessoa)
        {
            try
            {
                if (Pessoas.Any(p => p.Codigo == pessoa.Codigo))
                    return new RespostaDeRequisicao(HttpStatusCode.Conflict, MensagensConstantes.RegistroJaExistenteAoTentarCadastrar);

                var errosDeValidacao = ObtenhaInformacoesInvalidasDePessoa(pessoa);
                if (errosDeValidacao.Any())
                    return new RespostaDeRequisicao(HttpStatusCode.BadRequest, errosDeValidacao);

                if (pessoa.Codigo == 0)
                    pessoa.Codigo = Pessoas.Max(c => c.Codigo) + 1;

                pessoa.CPF = FuncoesDeFormatacao.FormatarCPF(pessoa.CPF);

                Pessoas.Add(pessoa);

                return new RespostaDeRequisicao(HttpStatusCode.Created, pessoa);
            }
            catch (Exception) 
            {
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }

        public RespostaDeRequisicao AtualizarPessoa(Pessoa pessoa) 
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == pessoa.Codigo);
                if (pessoaPersistida == null)
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.RegistroNaoEncontrado);

                var errosDeValidacao = ObtenhaInformacoesInvalidasDePessoa(pessoa);
                if (errosDeValidacao.Any())
                    return new RespostaDeRequisicao(HttpStatusCode.BadRequest, errosDeValidacao);

                pessoaPersistida.UF = pessoa.UF;
                pessoaPersistida.CPF = FuncoesDeFormatacao.FormatarCPF(pessoa.CPF);
                pessoaPersistida.DataDeNascimento = pessoa.DataDeNascimento;
                pessoaPersistida.Nome = pessoa.Nome;

                return new RespostaDeRequisicao(HttpStatusCode.OK, pessoaPersistida);
            }
            catch (Exception) 
            { 
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }

        public RespostaDeRequisicao ExcluirPessoa(int codigoPessoa)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == codigoPessoa);
                if (pessoaPersistida == null)
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.RegistroNaoEncontrado);

                Pessoas.Remove(pessoaPersistida);

                return new RespostaDeRequisicao(HttpStatusCode.OK, MensagensConstantes.RegistroRemovido);
            }
            catch (Exception) 
            {
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor);
            }
        }

        private Dictionary<string, string[]> ObtenhaInformacoesInvalidasDePessoa(Pessoa pessoa)
        {
            Dictionary<string, string[]> errosDeValidacao = new();

            if (!FuncoesDeValidacao.CPFValido(pessoa.CPF))
                errosDeValidacao.Add(nameof(pessoa.CPF), new string[] { "O CPF informado é inválido." });

            if (pessoa.DataDeNascimento?.Date >= DateTime.Now.Date)
                errosDeValidacao.Add(nameof(pessoa.DataDeNascimento), new string[] { "A data de nascimento não pode ser maior que a data atual." });

            return errosDeValidacao;
        }
    }
}

using Globaltec.Dominio.Autenticacao;
using Globaltec.Dominio.Constantes;
using Globaltec.Dominio.Modelos;
using Globaltec.Servicos.Servicos.Interfaces;
using System.Net;

namespace Globaltec.Servicos.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        /// <summary>
        /// Lista de usuários salvos na aplicação. Utilizado somente para fins de teste.
        /// </summary>
        private static List<Usuario> Usuarios = new() { new Usuario(1, "globaltec", "globaltec") };

        public RespostaDeRequisicao GraveUsuario(Credenciais credenciais)
        {
            try
            {
                if (Usuarios.Any(u => u.Login.Trim().ToUpper().Equals(credenciais.Usuario.Trim().ToUpper())))
                    return new RespostaDeRequisicao(HttpStatusCode.Conflict, "Esse usuário já está sendo utilizado.");

                var novoUsuario = new Usuario(Usuarios.Max(c => c.Codigo) + 1, credenciais.Usuario, credenciais.Senha);
                Usuarios.Add(novoUsuario);

                novoUsuario.Token = GeradorDeToken.GerarTokenDeAutenticacao(novoUsuario.Login);

                return new RespostaDeRequisicao(HttpStatusCode.Created, novoUsuario);
            }
            catch (Exception) 
            { 
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }

        public RespostaDeRequisicao ConsulteUsuarioPorLoginESenha(Credenciais credenciais)
        {
            try
            {
                var usuarioPersistido = Usuarios.FirstOrDefault(u => u.Login.Trim().ToUpper().Equals(credenciais.Usuario.Trim().ToUpper()) && u.Senha.Equals(credenciais.Senha));
                if (usuarioPersistido == null)
                    return new RespostaDeRequisicao(HttpStatusCode.NotFound, MensagensConstantes.UsuarioOuSenhaIncorretos);

                if (usuarioPersistido != null)
                    usuarioPersistido.Token = GeradorDeToken.GerarTokenDeAutenticacao(credenciais.Usuario);

                return new RespostaDeRequisicao(HttpStatusCode.OK, usuarioPersistido!);
            }
            catch (Exception)
            { 
                return new RespostaDeRequisicao(HttpStatusCode.InternalServerError, MensagensConstantes.ErroInternoDoServidor); 
            }
        }
    }
}
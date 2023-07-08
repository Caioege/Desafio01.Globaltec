using Globaltec.Dominio.Modelos;

namespace Globaltec.Servicos.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        /// <summary>
        /// Valida os dados informados e grava um novo usuário na aplicação.
        /// </summary>
        /// <param name="credenciais">Credenciais para a criação de um novo usuário. Composto por usuário e senha.</param>
        /// <returns>Os dados do usuário que foi persistido. Veja <see cref="RespostaDeRequisicao"/> e <see cref="Usuario"/>.</returns>
        RespostaDeRequisicao GraveUsuario(Credenciais credenciais);

        /// <summary>
        /// Consulta um usuário.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para autenticação.</param>
        /// <returns>Os dados do usuário persistido, juntamente com um token de autenticação para a API. Veja <see cref="RespostaDeRequisicao"/> e <see cref="Usuario"/>.</returns>
        RespostaDeRequisicao ConsulteUsuarioPorLoginESenha(Credenciais credenciais);
    }
}

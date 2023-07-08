namespace Globaltec.Servicos.Funcoes
{
    public static class FuncoesDeFormatacao
    {
        /// <summary>
        /// Aplica a máscara de CPF.
        /// </summary>
        /// <param name="cpf">CPF a ser formatado.</param>
        /// <returns>Ex: 111.222.333-44</returns>
        public static string FormatarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            return Convert.ToUInt64(cpf.Replace(".", "").Replace("-", "")).ToString(@"000\.000\.000\-00");
        }
    }
}

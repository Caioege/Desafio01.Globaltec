using System.Net;

namespace Globaltec.Dominio.Modelos
{
    public class RespostaDeRequisicao
    {
        public RespostaDeRequisicao(HttpStatusCode codigoHTTP, object resultado)
        {
            CodigoHTTP = (int)codigoHTTP;
            Resultado = resultado;
        }

        public int CodigoHTTP { get; set; }
        public object Resultado { get; set; }
    }
}
using Globaltec.Dominio.Constantes;
using System.ComponentModel.DataAnnotations;

namespace Globaltec.Dominio.Modelos
{
    public class Pessoa
    {
        public Pessoa() { }

        public Pessoa(int codigoPessoa, string nome, string cpf, string uf, DateTime dataDeNascimento)
        {
            Codigo = codigoPessoa;
            Nome = nome;
            CPF = cpf;
            UF = uf;
            DataDeNascimento = dataDeNascimento;
        }

        public int Codigo { get; set; }

        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio), MaxLength(150, ErrorMessage = "Máximo de 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio), MaxLength(14, ErrorMessage = "Máximo de 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio), MaxLength(2, ErrorMessage = "Máximo de 2 caracteres.")]
        public string UF { get; set; }

        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio)]
        public DateTime? DataDeNascimento { get; set; }
    }
}
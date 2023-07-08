using Globaltec.Dominio.Constantes;
using System.ComponentModel.DataAnnotations;

namespace Globaltec.Dominio.Modelos
{
    public class Credenciais
    {
        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = MensagensConstantes.CampoObrigatorio)]
        public string Senha { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMS.Models
{
    public class PrefeituraBairro
    {
        [Key]
        public int PrefeituraBairroId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Referencia { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Gestor { get; set; }

        public virtual ICollection<OcorrenciaPB> OcorrenciaPB { get; set; }

    }

}
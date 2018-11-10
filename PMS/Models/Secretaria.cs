using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMS.Models
{
    
    public class Secretaria
    {
       
        [Key]
        public int SecretariaId { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<Gre> Gre { get; set; }
    }
}
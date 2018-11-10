using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PMS.Models
{
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }
        [Required(ErrorMessage = "*")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoCargoId { get; set; }


        public virtual TipoCargo TipoCargo { get; set; }

        public virtual ICollection<Terceirizado> Terceirizado { get; set; }
    }
}
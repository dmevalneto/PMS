using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMS.Models
{
    public class TipoCargo
    {
        [Key]
        public int TipoCargoId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Cargo> Cargo { get; set; }
    }
}
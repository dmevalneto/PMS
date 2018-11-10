using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Ocorrencia
    {
        [Key]
        public int OcorrenciaId { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Terceirizado> Terceirizado { get; set; }
    }
}
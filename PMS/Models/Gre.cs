using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Gre
    {
        [Key]
        public int GreId { get; set; }
        public string Regional { get; set; }
        public int Codigo { get; set; }
        public string Endereco { get; set; }
        public string Referencia { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Gestor { get; set; }

        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }

        public virtual ICollection<Escola> Escola { get; set; }

        public virtual ICollection<OcorrenciaGre> OcorrenciaGre { get; set; }
    }
}
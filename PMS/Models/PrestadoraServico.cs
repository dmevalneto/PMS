using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class PrestadoraServico
    {
        [Key]
        public int PrestadoraServicoId { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Terceirizado> Terceirizado { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Escola
    {
        [Key]
        public int EscolaId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public float Cep { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public float Numero { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Gestor { get; set; }

        public int GreId { get; set; }
        public virtual Gre Gre { get; set; }
    }
}
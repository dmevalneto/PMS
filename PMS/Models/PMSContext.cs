using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class PMSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PMSContext() : base("name=PMSContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<PMS.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Secretaria> Secretarias { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Gre> Gres { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Escola> Escolas { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.TipoLotacao> TipoLotacaos { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.PrestadoraServico> PrestadoraServicoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Ocorrencia> Ocorrencias { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.TipoCargo> TipoCargoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Cargo> Cargoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.PrefeituraBairro> PrefeituraBairroes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Terceirizado> Terceirizadoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.Encaminhamento> Encaminhamentoes { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.TipoOcorrenciaPB> TipoOcorrenciaPBs { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.DescricaoOcorrenciaPB> DescricaoOcorrenciaPBs { get; set; }

        public System.Data.Entity.DbSet<PMS.Models.OcorrenciaPB> OcorrenciaPBs { get; set; }
    }
}

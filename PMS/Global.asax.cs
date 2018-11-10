using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PMS.Models;

namespace PMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.PMSContext, Migrations.Configuration>());
            ApplicationDbContext db = new ApplicationDbContext();
            CriarRoles(db);
            CriarSuperUser(db);
            AddPermissoesSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissoesSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("dmevalneto@outlook.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!userManager.IsInRole(user.Id,"View"))
            {
                userManager.AddToRole(user.Id, "View");
            }
            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }
            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }
            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
            if (!userManager.IsInRole(user.Id, "Secretaria"))
            {
                userManager.AddToRole(user.Id, "Secretaria");
            }
            if (!userManager.IsInRole(user.Id, "PrefeituraBairro"))
            {
                userManager.AddToRole(user.Id, "PrefeituraBairro");
            }
            if (!userManager.IsInRole(user.Id, "GRE"))
            {
                userManager.AddToRole(user.Id, "GRE");
            }
            if (!userManager.IsInRole(user.Id, "Escola"))
            {
                userManager.AddToRole(user.Id, "Escola");
            }
            if (!userManager.IsInRole(user.Id, "Administrador"))
            {
                userManager.AddToRole(user.Id, "Administrador");
            }

            // secretarias
            if (!userManager.IsInRole(user.Id, "SMED"))
            {
                userManager.AddToRole(user.Id, "SMED");
            }
            //fim secretarias 

            // PrefeiturasBairros
            if (!userManager.IsInRole(user.Id, "Centro/Brotas"))
            {
                userManager.AddToRole(user.Id, "Centro/Brotas");
            }
            if (!userManager.IsInRole(user.Id, "Suburbio/Ilhas"))
            {
                userManager.AddToRole(user.Id, "Suburbio/Ilhas");
            }
            if (!userManager.IsInRole(user.Id, "Cajazeiras"))
            {
                userManager.AddToRole(user.Id, "Cajazeiras");
            }
            if (!userManager.IsInRole(user.Id, "Itapua"))
            {
                userManager.AddToRole(user.Id, "Itapua");
            }
            if (!userManager.IsInRole(user.Id, "CidadeBaixa"))
            {
                userManager.AddToRole(user.Id, "CidadeBaixa");
            }
            if (!userManager.IsInRole(user.Id, "Barra/Pituba"))
            {
                userManager.AddToRole(user.Id, "Barra/Pituba");
            }
            if (!userManager.IsInRole(user.Id, "Cabula/TancredoNeves"))
            {
                userManager.AddToRole(user.Id, "Cabula/TancredoNeves");
            }
            if (!userManager.IsInRole(user.Id, "PaudaLima"))
            {
                userManager.AddToRole(user.Id, "PaudaLima");
            }
            if (!userManager.IsInRole(user.Id, "Liberdade/SaoCaetano"))
            {
                userManager.AddToRole(user.Id, "Liberdade/SaoCaetano");
            }
            if (!userManager.IsInRole(user.Id, "Valeria"))
            {
                userManager.AddToRole(user.Id, "Valeria");
            }
            //fim PrefeiturasBairros 

            // GRE
            if (!userManager.IsInRole(user.Id, "CABULA"))
            {
                userManager.AddToRole(user.Id, "CABULA");
            }
            if (!userManager.IsInRole(user.Id, "CENTRO"))
            {
                userManager.AddToRole(user.Id, "CENTRO");
            }
            if (!userManager.IsInRole(user.Id, "CIDADEBAIXA"))
            {
                userManager.AddToRole(user.Id, "CIDADEBAIXA");
            }
            if (!userManager.IsInRole(user.Id, "ITAPUA"))
            {
                userManager.AddToRole(user.Id, "ITAPUA");
            }
            if (!userManager.IsInRole(user.Id, "LIBERDADE"))
            {
                userManager.AddToRole(user.Id, "LIBERDADE");
            }
            if (!userManager.IsInRole(user.Id, "ORLA"))
            {
                userManager.AddToRole(user.Id, "ORLA");
            }
            if (!userManager.IsInRole(user.Id, "PIRAJA"))
            {
                userManager.AddToRole(user.Id, "PIRAJA");
            }
            if (!userManager.IsInRole(user.Id, "SAOCAETANO"))
            {
                userManager.AddToRole(user.Id, "SAOCAETANO");
            }
            if (!userManager.IsInRole(user.Id, "SUBURBIOI"))
            {
                userManager.AddToRole(user.Id, "SUBURBIOI");
            }
            if (!userManager.IsInRole(user.Id, "SUBURBIOII"))
            {
                userManager.AddToRole(user.Id, "SUBURBIOII");
            }
            //fim GRE 
        }

        private void CriarSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("dmevalneto@outlook.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "dmevalneto@outlook.com",
                    Email = "dmevalneto@outlook.com",

                };
                userManager.Create(user, "DmevalNeto@123");
            }
        }

        private void CriarRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }
            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
            if (!roleManager.RoleExists("Secretaria"))
            {
                roleManager.Create(new IdentityRole("Secretaria"));
            }
            if (!roleManager.RoleExists("PrefeituraBairro"))
            {
                roleManager.Create(new IdentityRole("PrefeituraBairro"));
            }
            if (!roleManager.RoleExists("GRE"))
            {
                roleManager.Create(new IdentityRole("GRE"));
            }
            if (!roleManager.RoleExists("Escola"))
            {
                roleManager.Create(new IdentityRole("Escola"));
            }
            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }

            // secretarias
            if (!roleManager.RoleExists("SMED"))
            {
                roleManager.Create(new IdentityRole("SMED"));
            }
            //fim secretarias 

            // PrefeiturasBairros
            if (!roleManager.RoleExists("Centro/Brotas"))
            {
                roleManager.Create(new IdentityRole("Centro/Brotas"));
            }
            if (!roleManager.RoleExists("Suburbio/Ilhas"))
            {
                roleManager.Create(new IdentityRole("Suburbio/Ilhas"));
            }
            if (!roleManager.RoleExists("Cajazeiras"))
            {
                roleManager.Create(new IdentityRole("Cajazeiras"));
            }
            if (!roleManager.RoleExists("Itapua"))
            {
                roleManager.Create(new IdentityRole("Itapua"));
            }
            if (!roleManager.RoleExists("CidadeBaixa"))
            {
                roleManager.Create(new IdentityRole("CidadeBaixa"));
            }
            if (!roleManager.RoleExists("Barra/Pituba"))
            {
                roleManager.Create(new IdentityRole("Barra/Pituba"));
            }
            if (!roleManager.RoleExists("Cabula/TancredoNeves"))
            {
                roleManager.Create(new IdentityRole("Cabula/TancredoNeves"));
            }
            if (!roleManager.RoleExists("PaudaLima"))
            {
                roleManager.Create(new IdentityRole("PaudaLima"));
            }
            if (!roleManager.RoleExists("Liberdade/SaoCaetano"))
            {
                roleManager.Create(new IdentityRole("Liberdade/SaoCaetano"));
            }
            if (!roleManager.RoleExists("Valeria"))
            {
                roleManager.Create(new IdentityRole("Valeria"));
            }
            //fim PrefeiturasBairros 

            // secretarias
            if (!roleManager.RoleExists("CABULA"))
            {
                roleManager.Create(new IdentityRole("CABULA"));
            }
            if (!roleManager.RoleExists("CENTRO"))
            {
                roleManager.Create(new IdentityRole("CENTRO"));
            }
            if (!roleManager.RoleExists("CIDADEBAIXA"))
            {
                roleManager.Create(new IdentityRole("CIDADEBAIXA"));
            }
            if (!roleManager.RoleExists("ITAPUA"))
            {
                roleManager.Create(new IdentityRole("ITAPUA"));
            }
            if (!roleManager.RoleExists("LIBERDADE"))
            {
                roleManager.Create(new IdentityRole("LIBERDADE"));
            }
            if (!roleManager.RoleExists("ORLA"))
            {
                roleManager.Create(new IdentityRole("ORLA"));
            }
            if (!roleManager.RoleExists("PIRAJA"))
            {
                roleManager.Create(new IdentityRole("PIRAJA"));
            }
            if (!roleManager.RoleExists("SAOCAETANO"))
            {
                roleManager.Create(new IdentityRole("SAOCAETANO"));
            }
            if (!roleManager.RoleExists("SUBURBIOI"))
            {
                roleManager.Create(new IdentityRole("SUBURBIOI"));
            }
            if (!roleManager.RoleExists("SUBURBIOII"))
            {
                roleManager.Create(new IdentityRole("SUBURBIOII"));
            }
            //fim secretarias 
        }
    }
}

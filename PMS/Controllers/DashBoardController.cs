using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class DashBoardController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: DashBoard
        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "View")]
        [Authorize(Roles = "Secretaria")]
        public ActionResult ListarSecretaria()
        {
            return View(db.Secretarias.ToList());
        }

        [Authorize(Roles = "View")]
        [Authorize(Roles = "PrefeituraBairro")]
        public ActionResult ListarPrefeituraBairro()
        {
            return View(db.PrefeituraBairroes.ToList());
        }

        [Authorize(Roles = "View")]
        [Authorize(Roles = "GRE")]
        public ActionResult ListarGre()
        {
            return View(db.Gres.ToList());
        }

        [Authorize(Roles = "View")]
        [Authorize(Roles = "Escola")]
        public ActionResult ListarEscola()
        {
            return View(db.Escolas.ToList());
        }

        [Authorize(Roles = "Secretaria")]
        public ActionResult DashBoardSecretaria()
        {
            return View();
        }

        [Authorize(Roles = "PrefeituraBairro")]
        public ActionResult DashBoadPrefeituraBairro()
        {
            return View();
        }

        [Authorize(Roles = "GRE")]
        public ActionResult DashBoadGre()
        {
            return View();
        }

        [Authorize(Roles = "Escola")]
        public ActionResult DashBoadEscola()
        {
            return View();
        }
    }
}
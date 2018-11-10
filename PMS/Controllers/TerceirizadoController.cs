using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMS.Models;

namespace PMS.Controllers
{
    public class TerceirizadoController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            var terceirizadoes = db.Terceirizadoes.Include(t => t.Cargo).Include(t => t.Ocorrencia).Include(t => t.PrestadoraServico);
            return View(terceirizadoes.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado terceirizado = db.Terceirizadoes.Find(id);
            if (terceirizado == null)
            {
                return HttpNotFound();
            }
            return View(terceirizado);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "Nome");
            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias, "OcorrenciaId", "Descricao");
            ViewBag.PrestadoraServicoId = new SelectList(db.PrestadoraServicoes, "PrestadoraServicoId", "RazaoSocial");
            return View();
        }

        // POST: Terceirizado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "TerceirizadoId,Nome,Cpf,DataAdmissao,Telefone,PrestadoraServicoId,CargoId,OcorrenciaId")] Terceirizado terceirizado)
        {
            if (ModelState.IsValid)
            {
                db.Terceirizadoes.Add(terceirizado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "Nome", terceirizado.CargoId);
            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias, "OcorrenciaId", "Descricao", terceirizado.OcorrenciaId);
            ViewBag.PrestadoraServicoId = new SelectList(db.PrestadoraServicoes, "PrestadoraServicoId", "RazaoSocial", terceirizado.PrestadoraServicoId);
            return View(terceirizado);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado terceirizado = db.Terceirizadoes.Find(id);
            if (terceirizado == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "Nome", terceirizado.CargoId);
            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias, "OcorrenciaId", "Descricao", terceirizado.OcorrenciaId);
            ViewBag.PrestadoraServicoId = new SelectList(db.PrestadoraServicoes, "PrestadoraServicoId", "RazaoSocial", terceirizado.PrestadoraServicoId);
            return View(terceirizado);
        }

        // POST: Terceirizado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "TerceirizadoId,Nome,Cpf,DataAdmissao,Telefone,PrestadoraServicoId,CargoId,OcorrenciaId")] Terceirizado terceirizado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terceirizado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "Nome", terceirizado.CargoId);
            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias, "OcorrenciaId", "Descricao", terceirizado.OcorrenciaId);
            ViewBag.PrestadoraServicoId = new SelectList(db.PrestadoraServicoes, "PrestadoraServicoId", "RazaoSocial", terceirizado.PrestadoraServicoId);
            return View(terceirizado);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado terceirizado = db.Terceirizadoes.Find(id);
            if (terceirizado == null)
            {
                return HttpNotFound();
            }
            return View(terceirizado);
        }

        // POST: Terceirizado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Terceirizado terceirizado = db.Terceirizadoes.Find(id);
            db.Terceirizadoes.Remove(terceirizado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Encaminhamento()
        {

            return View();
        }

        public ActionResult ListarTerceirizados(string tipoLot, int idLot)
        {
            TempData["tipoLot"] = tipoLot;
            TempData.Keep("tipoLot");
            TempData["idLot"] = idLot;
            TempData.Keep("idLot");
            ViewBag.Titulo = "Selecione o terceirizado que deseja encaminhar.";
            return View(db.Terceirizadoes.ToList());
        }


        public ActionResult ListarSecretaria(int idTer)
        {
            TempData["idTer"] = idTer;
            TempData.Keep("idTer");
            ViewBag.Titulo = "Selecione a secretária que deseja alocar o terceirizado.";
            return View(db.Secretarias.ToList());
        }

        public ActionResult ListarTerceirizadosPB(string tipoLot, int idLot)
        {
            TempData["tipoLot"] = tipoLot;
            TempData.Keep("tipoLot");
            TempData["idLot"] = idLot;
            TempData.Keep("idLot");
            ViewBag.Titulo = "Selecione o terceirizado que deseja encaminhar.";
            return View(db.Terceirizadoes.ToList());
        }


        public ActionResult ListarPrefeituraBairro(int idTer)
        {
            TempData["idTer"] = idTer;
            TempData.Keep("idTer");
            ViewBag.Titulo = "Selecione a Prefeitura Bairro que deseja alocar o terceirizado.";
            return View(db.PrefeituraBairroes.ToList());
        }

        public ActionResult ListarTerceirizadosGre(string tipoLot, int idLot)
        {
            TempData["tipoLot"] = tipoLot;
            TempData.Keep("tipoLot");
            TempData["idLot"] = idLot;
            TempData.Keep("idLot");
            ViewBag.Titulo = "Selecione o terceirizado que deseja encaminhar.";
            return View(db.Terceirizadoes.ToList());
        }


        public ActionResult ListarGre(int idTer)
        {
            TempData["idTer"] = idTer;
            TempData.Keep("idTer");
            ViewBag.Titulo = "Selecione a GRE que deseja alocar o terceirizado.";
            return View(db.Gres.ToList());
        }


        public ActionResult ListarTerceirizadosEscola(string tipoLot, int idLot)
        {
            TempData["tipoLot"] = tipoLot;
            TempData.Keep("tipoLot");
            TempData["idLot"] = idLot;
            TempData.Keep("idLot");
            ViewBag.Titulo = "Selecione o terceirizado que deseja encaminhar.";
            return View(db.Terceirizadoes.ToList());
        }


        public ActionResult ListarEscola(int idTer)
        {
            TempData["idTer"] = idTer;
            TempData.Keep("idTer");
            ViewBag.Titulo = "Selecione a Escola que deseja alocar o terceirizado.";
            return View(db.Escolas.ToList());
        }


        public ActionResult ConfirmarEncaminhamento(int lotacaoId)
        {

            ViewBag.lotacaoId = lotacaoId;
            ViewBag.idLot = TempData["idLot"];
            ViewBag.idTer = TempData["idTer"];
            ViewBag.data = DateTime.Now;
            Encaminhamento enc = new Encaminhamento();
            enc.TerceirizadoId = ViewBag.idTer;
            enc.Data = DateTime.Now;
            enc.TipoLotacaoId = ViewBag.idLot;
            enc.LotacaoId = ViewBag.lotacaoId;

            // TODO: Add insert logic here
            db.Encaminhamentoes.Add(enc);
            db.SaveChanges();
            return View();

        }

        public ActionResult ListarEncaminhamentos()
        {
            return View(db.Encaminhamentoes.ToList().OrderByDescending(x => x.Data).Take(10));
        }

        public ActionResult ImprimirEncaminhamento(int? id)
        {
            
            ViewBag.NomeLotacao = "";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encaminhamento enc = db.Encaminhamentoes.Find(id);
            ViewBag.TerId = enc.TerceirizadoId;
            ViewBag.Data = enc.Data;
            ViewBag.TipoLotacaoId = enc.TipoLotacaoId;
            ViewBag.LotacaoId = enc.LotacaoId;

            Terceirizado ter = db.Terceirizadoes.Find(enc.TerceirizadoId);
            ViewBag.nomeTer = ter.Nome;
            ViewBag.cpfTer = ter.Cpf;
            ViewBag.telTer = ter.Telefone;
            ViewBag.empTerId = ter.PrestadoraServicoId;
            ViewBag.cargoTerId = ter.CargoId;

            PrestadoraServico pres = db.PrestadoraServicoes.Find(ViewBag.empTerId);
            ViewBag.NomeEmp = pres.RazaoSocial;

            Cargo car = db.Cargoes.Find(ViewBag.cargoTerId);
            ViewBag.NomeCargo = car.Nome;


            if (enc.TipoLotacaoId == 3)
            {
                Secretaria sec = db.Secretarias.Find(enc.LotacaoId);
                ViewBag.NomeLotacao = sec.Nome;
            }
            if (enc.TipoLotacaoId == 4)
            {
                PrefeituraBairro pre = db.PrefeituraBairroes.Find(enc.LotacaoId);
                ViewBag.NomeLotacao = pre.Nome;
            }
            if (enc.TipoLotacaoId == 5)
            {
                Gre gre = db.Gres.Find(enc.LotacaoId);
                ViewBag.NomeLotacao = gre.Regional;
            }
            if (enc.TipoLotacaoId == 6)
            {
                Escola esc = db.Escolas.Find(enc.LotacaoId);
                ViewBag.NomeLotacao = esc.Nome;
                ViewBag.endEscola = esc.Logradouro;
            }

            if (enc == null)
            {
                return HttpNotFound();
            }

            return View();
        }

      


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

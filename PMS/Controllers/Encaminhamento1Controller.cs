using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace PMS.Controllers
{
    public class Encaminhamento1Controller : Controller
    {
        private PMSContext db = new PMSContext();
        // GET: Encaminhamento1
        public ActionResult Index()
        {
            var terceirizadoes = db.Terceirizadoes.Include(t => t.Cargo).Include(t => t.Ocorrencia).Include(t => t.PrestadoraServico);
            return View(terceirizadoes.ToList());
        }

        // GET: Encaminhamento1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Encaminhamento1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encaminhamento1/Create
        [HttpPost]
        public ActionResult Create(Encaminhamento enc)
        {
            try
            {
                // TODO: Add insert logic here
                db.Encaminhamentoes.Add(enc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Encaminhamento1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encaminhamento1/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Encaminhamento1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Encaminhamento1/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SelecionarSecretaria(int? id)
        {
            TempData["tipoLotacaoId"] = 3;
            TempData.Keep("tipoLotacaoId");
            //string nomeTer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado ter = db.Terceirizadoes.Find(id);
            if (ter == null)
            {
                return HttpNotFound();
            }

            TempData["idTer"] = ter.TerceirizadoId;
            TempData.Keep("idTer");
            return View(db.Secretarias.ToList());
        }

        public ActionResult SelecionarPrefeituraBairro(int? id)
        {
            TempData["tipoLotacaoId"] = 4;
            TempData.Keep("tipoLotacaoId");
            //string nomeTer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado ter = db.Terceirizadoes.Find(id);
            if (ter == null)
            {
                return HttpNotFound();
            }

            TempData["idTer"] = ter.TerceirizadoId;
            TempData.Keep("idTer");
            return View(db.PrefeituraBairroes.ToList());
        }

        public ActionResult SelecionarGre(int? id)
        {
            TempData["tipoLotacaoId"] = 5;
            TempData.Keep("tipoLotacaoId");
            //string nomeTer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado ter = db.Terceirizadoes.Find(id);
            if (ter == null)
            {
                return HttpNotFound();
            }

            TempData["idTer"] = ter.TerceirizadoId;
            TempData.Keep("idTer");
            return View(db.Gres.ToList());
        }

        public ActionResult SelecionarEscola(int? id)
        {
            TempData["tipoLotacaoId"] = 6;
            TempData.Keep("tipoLotacaoId");
            //string nomeTer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terceirizado ter = db.Terceirizadoes.Find(id);
            if (ter == null)
            {
                return HttpNotFound();
            }

            TempData["idTer"] = ter.TerceirizadoId;
            TempData.Keep("idTer");
            return View(db.Escolas.ToList());
        }
        public ActionResult ConfirmarEncaminhamento(int? id)
        {
            try
            {
                Secretaria sec = db.Secretarias.Find(id);
                ViewBag.nomeSec = sec.Nome;
                ViewBag.idSec = id;



                ViewBag.tipoLotacaoId = TempData["tipoLotacaoId"];
                TipoLotacao lot = db.TipoLotacaos.Find(ViewBag.tipoLotacaoId);
                ViewBag.nomeLot = lot.Nome;

              



                ViewBag.idTer = TempData["idTer"];
                Terceirizado ter = db.Terceirizadoes.Find(ViewBag.idTer);
                ViewBag.nomeTer = ter.Nome;
                ViewBag.cpfTer = ter.Cpf;
                ViewBag.dataAdmissaoTer = ter.DataAdmissao;
                ViewBag.telefoneTer = ter.Telefone;
                ViewBag.presTer = ter.PrestadoraServicoId;
                ViewBag.cargoIdTer = ter.CargoId;
                ViewBag.ocorrenciaIdTer = ter.OcorrenciaId;

                Encaminhamento enc = new Encaminhamento();
                enc.TerceirizadoId = ViewBag.idTer;
                enc.Data = DateTime.Now;
                ViewBag.DataEnc = enc.Data;
                enc.TipoLotacaoId = ViewBag.tipoLotacaoId;
                enc.LotacaoId = ViewBag.idSec;

                return View(enc);
            }
            catch (Exception)
            {

                return View("Index");
            }


        }

        public ActionResult RelacaoEncaminhamentos()
        {
            return View(db.Encaminhamentoes.ToList().OrderByDescending(x => x.Data));
        }

        public ActionResult ImprimirCarta(int? id)
        {
            Encaminhamento enc = db.Encaminhamentoes.Find(id);
            int lotacaoId = enc.LotacaoId;
            ViewBag.idTer = enc.TerceirizadoId;
            ViewBag.dataEnc = enc.Data;

            Terceirizado ter = db.Terceirizadoes.Find(enc.TerceirizadoId);
            ViewBag.nomeTer = ter.Nome;

            TipoLotacao lot = db.TipoLotacaos.Find(enc.TipoLotacaoId);
            ViewBag.tipoLot = lot.Nome;

            if (lot.Nome == "Secretaria")
            {
                Secretaria sec = db.Secretarias.Find(lotacaoId);
                ViewBag.nomeLot = sec.Nome;
            }
            else if (lot.Nome == "Prefeitura Bairro")
            {
                PrefeituraBairro pre = db.PrefeituraBairroes.Find(lotacaoId);
                ViewBag.nomeLot = pre.Nome;
            }
            else if (lot.Nome == "GRE")
            {
                Gre gre = db.Gres.Find(lotacaoId);
                ViewBag.nomeLot = gre.Regional;
            }
            else if (lot.Nome == "Escola")
            {
                Escola escola = db.Escolas.Find(lotacaoId);
                ViewBag.nomeLot = escola.Nome;
            }
            return View();
        }
    }
}

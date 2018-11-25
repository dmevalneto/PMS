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


    public class EncaminhamentoController : Controller
    {
        private PMSContext db = new PMSContext();
        private Encaminhamento encaminhamento = new Encaminhamento();
        // GET: Encaminhamento
        public ActionResult Index()
        {
            var terceirizadoes = db.Terceirizadoes.Include(t => t.Cargo).Include(t => t.Ocorrencia).Include(t => t.PrestadoraServico);
            return View(terceirizadoes.ToList());
        }

        // GET: Encaminhamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult teste()
        {
            var robotDogs = (from d in db.Terceirizadoes
                             join f in db.Cargoes
                             on d.CargoId equals f.CargoId
                             //where f.Location == "Texas"
                             select d).ToList();
            return View(robotDogs);
        }

        public ActionResult teste1()
        {
            var robotDogs = (from d in db.Terceirizadoes
                             join f in db.Escolas
                             on d.Lotacao equals f.EscolaId
                             //where f.Location == "Texas"
                             select d).ToList();
            return View(robotDogs);
        }

        public ActionResult teste2()
        {
            var robotDogs = (from d in db.Escolas
                             join f in db.Terceirizadoes
                             on d.EscolaId equals f.Lotacao
                             //where f.Location == "Texas"
                             select d).ToList();
            return View(robotDogs);
        }

        // GET: Encaminhamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encaminhamento/Create
        [HttpPost]
        public ActionResult Create(Encaminhamento encaminhamento)
        {
            try
            {
                // TODO: Add insert logic here
                db.Encaminhamentoes.Add(encaminhamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Encaminhamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encaminhamento/Edit/5
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

        // GET: Encaminhamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Encaminhamento/Delete/5
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


        public ActionResult SelecionarSecretaria(int idTer, string nomeTer, int tipoLotacaoId)
        {
            ViewBag.Title = "Selecione uma Prefeitura Bairro";
            ViewBag.Lotacao = "Prefeitura Bairro";
            //ViewBag.id = id;
            //ViewBag.Nome = nome;
            //ViewBag.DataAtual = DateTime.Now;
            //ViewBag.TipoLotacao = TipoLotacaoId;

            encaminhamento.TerceirizadoId = idTer;
            encaminhamento.Data = DateTime.Now;
            encaminhamento.TipoLotacaoId = tipoLotacaoId;
            TempData["Encaminhamento"] = encaminhamento;
            TempData.Keep("encaminhamento");
            return View(db.Secretarias.ToList());
        }


        public ActionResult SelecionarPrefeituraBairro(int idTer, string nomeTer, int tipoLotacaoId)
        {
            ViewBag.Title = "Selecione uma Prefeitura Bairro";
            ViewBag.Lotacao = "Prefeitura Bairro";
            //ViewBag.id = id;
            //ViewBag.Nome = nome;
            //ViewBag.DataAtual = DateTime.Now;
            //ViewBag.TipoLotacao = TipoLotacaoId;

            encaminhamento.TerceirizadoId = idTer;
            encaminhamento.Data = DateTime.Now;
            encaminhamento.TipoLotacaoId = tipoLotacaoId;
            TempData["Encaminhamento"] = encaminhamento;
            TempData.Keep("encaminhamento");
            return View(db.PrefeituraBairroes.ToList());
        }

        public ActionResult SelecionarGre(int idTer, string nomeTer, int tipoLotacaoId)
        {
            ViewBag.Title = "Selecione uma Prefeitura Bairro";
            ViewBag.Lotacao = "Prefeitura Bairro";
            //ViewBag.id = id;
            //ViewBag.Nome = nome;
            //ViewBag.DataAtual = DateTime.Now;
            //ViewBag.TipoLotacao = TipoLotacaoId;

            encaminhamento.TerceirizadoId = idTer;
            encaminhamento.Data = DateTime.Now;
            encaminhamento.TipoLotacaoId = tipoLotacaoId;
            TempData["Encaminhamento"] = encaminhamento;
            TempData.Keep("encaminhamento");
            return View(db.Gres.ToList());
        }

        public ActionResult SelecionarEscola(int idTer, string nomeTer, int tipoLotacaoId)
        {
            ViewBag.Title = "Selecione uma Prefeitura Bairro";
            ViewBag.Lotacao = "Prefeitura Bairro";
            //ViewBag.id = id;
            //ViewBag.Nome = nome;
            //ViewBag.DataAtual = DateTime.Now;
            //ViewBag.TipoLotacao = TipoLotacaoId;

            encaminhamento.TerceirizadoId = idTer;
            encaminhamento.Data = DateTime.Now;
            encaminhamento.TipoLotacaoId = tipoLotacaoId;
            TempData["Encaminhamento"] = encaminhamento;
            TempData.Keep("encaminhamento");
            return View(db.Escolas.ToList());
        }

        public ActionResult ConfirmarEncaminhamento(int id)
        {
            try
            {
                encaminhamento = (Encaminhamento)TempData["Encaminhamento"];
                encaminhamento.LotacaoId = id;
                return View();
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }

        }

        public ActionResult CartaEncaminhamento()
        {
            return View(db.Encaminhamentoes.ToList().OrderByDescending(x => x.Data));
        }

        public ActionResult ImpCartaEncaminhamento(int? id)
        {

            //if (tipoLotacao == "Secretaria")
            //{
            //    //Encaminhamento encaminhamento = db.Encaminhamentoes.Find(id);
            //    var queryEnSec = db.Encaminhamentoes.Join(db.Secretarias, e => e.LotacaoId, s => s.SecretariaId, (e, s) => new { e, s }).Where(x => x.e.EncaminhamentoId == x.s.SecretariaId);
            //    return View(queryEnSec);
            //}
            //else if (tipoLotacao == "Prefeitura Bairro")
            //{
            //    return View(tipoLotacao);
            //}
            //else if (tipoLotacao == "GRE")
            //{
            //    return View(tipoLotacao);
            //}
            //else if (tipoLotacao == "Escola")
            //{
            //    return View(tipoLotacao);
            //}

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encaminhamento encaminhamento = db.Encaminhamentoes.Find(id);
            if (encaminhamento == null)
            {
                return HttpNotFound();
            }
            return View(encaminhamento);


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

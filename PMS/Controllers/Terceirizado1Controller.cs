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
    public class Terceirizado1Controller : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: Terceirizado1
        public ActionResult Index()
        {
            var terceirizadoes = db.Terceirizadoes.Include(t => t.Cargo).Include(t => t.Ocorrencia).Include(t => t.PrestadoraServico);
            return View(terceirizadoes.ToList().OrderBy(x => x.Nome));
        }

        // GET: Terceirizado1/Details/5
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

        // GET: Terceirizado1/Create
        public ActionResult Create()
        {
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "Nome");
            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias, "OcorrenciaId", "Descricao");
            ViewBag.PrestadoraServicoId = new SelectList(db.PrestadoraServicoes, "PrestadoraServicoId", "RazaoSocial");
            return View();
        }

        // POST: Terceirizado1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TerceirizadoId,Nome,Cpf,DataAdmissao,Telefone,TipoLotacao,Lotacao,PrestadoraServicoId,CargoId,OcorrenciaId")] Terceirizado terceirizado)
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

        // GET: Terceirizado1/Edit/5
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

        // POST: Terceirizado1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TerceirizadoId,Nome,Cpf,DataAdmissao,Telefone,TipoLotacao,Lotacao,PrestadoraServicoId,CargoId,OcorrenciaId")] Terceirizado terceirizado)
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

        // GET: Terceirizado1/Delete/5
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

        // POST: Terceirizado1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Terceirizado terceirizado = db.Terceirizadoes.Find(id);
            db.Terceirizadoes.Remove(terceirizado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EncaminharP1(int id, string nome)
        {
            TempData["id"] = id;
            TempData.Keep("id");
            TempData["nome"] = nome;
            TempData.Keep("nome");

            ViewBag.nome = nome;
            ViewBag.id = id;
            return View();
        }

        public ActionResult ListarSecretaria(int id)
        {
            if (id == 3)
            {
                return View(db.Secretarias.ToList());
            }
            else if (id == 4)
            {
                return RedirectToAction("ListarPrefeituraBairro");
            }
            else if (id == 5)
            {
                return RedirectToAction("ListarGre");
            }
            else if (id == 6)
            {
                return RedirectToAction("Listarescola");
            }
            return View();
        }


        public ActionResult ListarPrefeituraBairro()
        {
            return View(db.PrefeituraBairroes.ToList());
        }

        public ActionResult ListarGre()
        {
            return View(db.Gres.ToList());
        }

        public ActionResult Listarescola()
        {
            return View(db.Escolas.ToList());
        }


        public ActionResult EncaminharSecretaria(int id)
        {
            try
            {
                ViewBag.id = TempData["id"];
                int idTer = ViewBag.id;
                Encaminhamento enc = new Encaminhamento();
                enc.TerceirizadoId = ViewBag.id;
                enc.Data = DateTime.Now;
                enc.TipoLotacaoId = 3;
                enc.LotacaoId = id;
                db.Encaminhamentoes.Add(enc);
                db.SaveChanges();


                Terceirizado ter = db.Terceirizadoes.First(t => t.TerceirizadoId == idTer);
                ter.TipoLotacao = 3;
                ter.Lotacao = id;
                db.Entry(ter).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }

        public ActionResult EncaminharPrefeituraBairro(int id)
        {
            try
            {
                ViewBag.id = TempData["id"];
                int idTer = ViewBag.id;
                Encaminhamento enc = new Encaminhamento();
                enc.TerceirizadoId = ViewBag.id;
                enc.Data = DateTime.Now;
                enc.TipoLotacaoId = 4;
                enc.LotacaoId = id;
                db.Encaminhamentoes.Add(enc);
                db.SaveChanges();


                Terceirizado ter = db.Terceirizadoes.First(t => t.TerceirizadoId == idTer);
                ter.TipoLotacao = 4;
                ter.Lotacao = id;
                db.Entry(ter).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }

        public ActionResult EncaminharGre(int id)
        {
            try
            {
                ViewBag.id = TempData["id"];
                int idTer = ViewBag.id;
                Encaminhamento enc = new Encaminhamento();
                enc.TerceirizadoId = ViewBag.id;
                enc.Data = DateTime.Now;
                enc.TipoLotacaoId = 5;
                enc.LotacaoId = id;
                db.Encaminhamentoes.Add(enc);
                db.SaveChanges();


                Terceirizado ter = db.Terceirizadoes.First(t => t.TerceirizadoId == idTer);
                ter.TipoLotacao = 5;
                ter.Lotacao = id;
                db.Entry(ter).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }

        public ActionResult EncaminharEscola(int id)
        {
            try
            {
                ViewBag.id = TempData["id"];
                int idTer = ViewBag.id;
                Encaminhamento enc = new Encaminhamento();
                enc.TerceirizadoId = ViewBag.id;
                enc.Data = DateTime.Now;
                enc.TipoLotacaoId = 6;
                enc.LotacaoId = id;
                db.Encaminhamentoes.Add(enc);
                db.SaveChanges();


                Terceirizado ter = db.Terceirizadoes.First(t => t.TerceirizadoId == idTer);
                ter.TipoLotacao = 6;
                ter.Lotacao = id;
                db.Entry(ter).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


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


        public ActionResult Historico(int id)
        {
            var enc = db.Encaminhamentoes.Where(c => c.TerceirizadoId == id);
            return View(enc);
        }

        public ActionResult LocalRemanejamento(int id)
        {
            Encaminhamento enc = new Encaminhamento();
            enc = db.Encaminhamentoes.Find(id);
            int TipoLotacaoId = enc.TipoLotacaoId;
            TempData["LotacaoId"] = enc.LotacaoId;
            TempData.Keep("LotacaoId");

            if (TipoLotacaoId == 3)
            {
                return RedirectToAction("LotSecretaria");
            }
            if (TipoLotacaoId == 4)
            {
                return RedirectToAction("LotPrefeituraBairro");
            }
            if (TipoLotacaoId == 5)
            {
                return RedirectToAction("LotGre");
            }
            if (TipoLotacaoId == 6)
            {
                return RedirectToAction("LotEscola");
            }
            return View();
        }

        public ActionResult LotSecretaria()
        {
            var sec = db.Secretarias.Find(TempData["LotacaoId"]);
            return View(sec);
        }

        public ActionResult LotPrefeituraBairro()
        {
            var pb = db.PrefeituraBairroes.Find(TempData["LotacaoId"]);
            return View(pb);
        }

        public ActionResult LotGre()
        {
            var gre = db.Gres.Find(TempData["LotacaoId"]);
            return View(gre);
        }

        public ActionResult LotEscola()
        {
            var escola = db.Escolas.Find(TempData["LotacaoId"]);
            return View(escola);
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

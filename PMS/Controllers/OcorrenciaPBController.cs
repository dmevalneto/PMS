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
    public class OcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: OcorrenciaPB
        public ActionResult Index()
        {
            var ocorrenciaPBs = db.OcorrenciaPBs.Include(o => o.DescricaoOcorrenciaPB).Include(o => o.PrefeituraBairro).Include(o => o.TipoOcorrenciaPB);
            return View(ocorrenciaPBs.ToList().OrderByDescending(d => d.Data));
        }

        // GET: OcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaPB);
        }

        // GET: OcorrenciaPB/Create
        public ActionResult Create()
        {
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao");
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome");
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo");
            return View();
        }

        // POST: OcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaPBId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaPBId,DescricaoOcorrenciaPBId,PrefeituraBairroId")] OcorrenciaPB ocorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                ocorrenciaPB.Data = DateTime.Now;
                db.OcorrenciaPBs.Add(ocorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // GET: OcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // POST: OcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaPBId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaPBId,DescricaoOcorrenciaPBId,PrefeituraBairroId")] OcorrenciaPB ocorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // GET: OcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaPB);
        }

        // POST: OcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            db.OcorrenciaPBs.Remove(ocorrenciaPB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SalvarHistoricoStatus(int id)
        {
            ViewBag.id = id;
            TempData["id"] = id;
            TempData.Keep("id");
            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude");
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarHistoricoStatus([Bind(Include = "HistoricoStatusOcorrenciaPBId,Data,Observacao,StatusOcorrenciaPBId,OcorrenciaPBId")] HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                ViewBag.id = TempData["id"];
                historicoStatusOcorrenciaPB.Data = DateTime.Now;
                historicoStatusOcorrenciaPB.OcorrenciaPBId = ViewBag.id;
                db.HistoricoStatusOcorrenciaPBs.Add(historicoStatusOcorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude", historicoStatusOcorrenciaPB.OcorrenciaPBId);
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status", historicoStatusOcorrenciaPB.StatusOcorrenciaPBId);
            return View(historicoStatusOcorrenciaPB);
        }

        public ActionResult HistoricoStatus(int id)
        {

            var hist = db.HistoricoStatusOcorrenciaPBs.Where(c => c.OcorrenciaPBId == id);

            return View(hist);
        }


        public ActionResult ViewMapa(int id)
        {

            var oco = db.OcorrenciaPBs.Where(c => c.PrefeituraBairroId == id);

            return View(oco);
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

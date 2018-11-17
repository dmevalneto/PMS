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
    public class HistoricoStatusOcorrenciaEscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: HistoricoStatusOcorrenciaEscola
        public ActionResult Index()
        {
            var historicoStatusOcorrenciaEscolas = db.HistoricoStatusOcorrenciaEscolas.Include(h => h.OcorrenciaEscola).Include(h => h.StatusOcorrenciaEscola);
            return View(historicoStatusOcorrenciaEscolas.ToList());
        }

        // GET: HistoricoStatusOcorrenciaEscola/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola = db.HistoricoStatusOcorrenciaEscolas.Find(id);
            if (historicoStatusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaEscola);
        }

        // GET: HistoricoStatusOcorrenciaEscola/Create
        public ActionResult Create()
        {
            ViewBag.OcorrenciaEscolaId = new SelectList(db.OcorrenciaEscolas, "OcorrenciaEscolaId", "latitude");
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status");
            return View();
        }

        // POST: HistoricoStatusOcorrenciaEscola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoricoStatusOcorrenciaEscolaId,Data,Observacao,StatusOcorrenciaEscolaId,OcorrenciaEscolaId")] HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.HistoricoStatusOcorrenciaEscolas.Add(historicoStatusOcorrenciaEscola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaEscolaId = new SelectList(db.OcorrenciaEscolas, "OcorrenciaEscolaId", "latitude", historicoStatusOcorrenciaEscola.OcorrenciaEscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", historicoStatusOcorrenciaEscola.StatusOcorrenciaEscolaId);
            return View(historicoStatusOcorrenciaEscola);
        }

        // GET: HistoricoStatusOcorrenciaEscola/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola = db.HistoricoStatusOcorrenciaEscolas.Find(id);
            if (historicoStatusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            ViewBag.OcorrenciaEscolaId = new SelectList(db.OcorrenciaEscolas, "OcorrenciaEscolaId", "latitude", historicoStatusOcorrenciaEscola.OcorrenciaEscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", historicoStatusOcorrenciaEscola.StatusOcorrenciaEscolaId);
            return View(historicoStatusOcorrenciaEscola);
        }

        // POST: HistoricoStatusOcorrenciaEscola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoricoStatusOcorrenciaEscolaId,Data,Observacao,StatusOcorrenciaEscolaId,OcorrenciaEscolaId")] HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicoStatusOcorrenciaEscola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OcorrenciaEscolaId = new SelectList(db.OcorrenciaEscolas, "OcorrenciaEscolaId", "latitude", historicoStatusOcorrenciaEscola.OcorrenciaEscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", historicoStatusOcorrenciaEscola.StatusOcorrenciaEscolaId);
            return View(historicoStatusOcorrenciaEscola);
        }

        // GET: HistoricoStatusOcorrenciaEscola/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola = db.HistoricoStatusOcorrenciaEscolas.Find(id);
            if (historicoStatusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaEscola);
        }

        // POST: HistoricoStatusOcorrenciaEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoricoStatusOcorrenciaEscola historicoStatusOcorrenciaEscola = db.HistoricoStatusOcorrenciaEscolas.Find(id);
            db.HistoricoStatusOcorrenciaEscolas.Remove(historicoStatusOcorrenciaEscola);
            db.SaveChanges();
            return RedirectToAction("Index");
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

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
    public class HistoricoStatusOcorrenciaSecController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: HistoricoStatusOcorrenciaSec
        public ActionResult Index()
        {
            var historicoStatusOcorrenciaSecs = db.HistoricoStatusOcorrenciaSecs.Include(h => h.OcorrenciaSec).Include(h => h.StatusOcorrenciaSec);
            return View(historicoStatusOcorrenciaSecs.ToList());
        }

        // GET: HistoricoStatusOcorrenciaSec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec = db.HistoricoStatusOcorrenciaSecs.Find(id);
            if (historicoStatusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaSec);
        }

        // GET: HistoricoStatusOcorrenciaSec/Create
        public ActionResult Create()
        {
            ViewBag.OcorrenciaSecId = new SelectList(db.OcorrenciaSecs, "OcorrenciaSecId", "latitude");
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status");
            return View();
        }

        // POST: HistoricoStatusOcorrenciaSec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoricoStatusOcorrenciaSecId,Data,Observacao,StatusOcorrenciaSecId,OcorrenciaSecId")] HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.HistoricoStatusOcorrenciaSecs.Add(historicoStatusOcorrenciaSec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaSecId = new SelectList(db.OcorrenciaSecs, "OcorrenciaSecId", "latitude", historicoStatusOcorrenciaSec.OcorrenciaSecId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", historicoStatusOcorrenciaSec.StatusOcorrenciaSecId);
            return View(historicoStatusOcorrenciaSec);
        }

        // GET: HistoricoStatusOcorrenciaSec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec = db.HistoricoStatusOcorrenciaSecs.Find(id);
            if (historicoStatusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            ViewBag.OcorrenciaSecId = new SelectList(db.OcorrenciaSecs, "OcorrenciaSecId", "latitude", historicoStatusOcorrenciaSec.OcorrenciaSecId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", historicoStatusOcorrenciaSec.StatusOcorrenciaSecId);
            return View(historicoStatusOcorrenciaSec);
        }

        // POST: HistoricoStatusOcorrenciaSec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoricoStatusOcorrenciaSecId,Data,Observacao,StatusOcorrenciaSecId,OcorrenciaSecId")] HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicoStatusOcorrenciaSec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OcorrenciaSecId = new SelectList(db.OcorrenciaSecs, "OcorrenciaSecId", "latitude", historicoStatusOcorrenciaSec.OcorrenciaSecId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", historicoStatusOcorrenciaSec.StatusOcorrenciaSecId);
            return View(historicoStatusOcorrenciaSec);
        }

        // GET: HistoricoStatusOcorrenciaSec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec = db.HistoricoStatusOcorrenciaSecs.Find(id);
            if (historicoStatusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaSec);
        }

        // POST: HistoricoStatusOcorrenciaSec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoricoStatusOcorrenciaSec historicoStatusOcorrenciaSec = db.HistoricoStatusOcorrenciaSecs.Find(id);
            db.HistoricoStatusOcorrenciaSecs.Remove(historicoStatusOcorrenciaSec);
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

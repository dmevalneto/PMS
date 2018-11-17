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
    public class HistoricoStatusOcorrenciaGreController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: HistoricoStatusOcorrenciaGre
        public ActionResult Index()
        {
            var historicoStatusOcorrenciaGres = db.HistoricoStatusOcorrenciaGres.Include(h => h.OcorrenciaGre).Include(h => h.StatusOcorrenciaGre);
            return View(historicoStatusOcorrenciaGres.ToList());
        }

        // GET: HistoricoStatusOcorrenciaGre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre = db.HistoricoStatusOcorrenciaGres.Find(id);
            if (historicoStatusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaGre);
        }

        // GET: HistoricoStatusOcorrenciaGre/Create
        public ActionResult Create()
        {
            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude");
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status");
            return View();
        }

        // POST: HistoricoStatusOcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoricoStatusOcorrenciaGreId,Data,Observacao,StatusOcorrenciaGreId,OcorrenciaGreId")] HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.HistoricoStatusOcorrenciaGres.Add(historicoStatusOcorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude", historicoStatusOcorrenciaGre.OcorrenciaGreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", historicoStatusOcorrenciaGre.StatusOcorrenciaGreId);
            return View(historicoStatusOcorrenciaGre);
        }

        // GET: HistoricoStatusOcorrenciaGre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre = db.HistoricoStatusOcorrenciaGres.Find(id);
            if (historicoStatusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude", historicoStatusOcorrenciaGre.OcorrenciaGreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", historicoStatusOcorrenciaGre.StatusOcorrenciaGreId);
            return View(historicoStatusOcorrenciaGre);
        }

        // POST: HistoricoStatusOcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoricoStatusOcorrenciaGreId,Data,Observacao,StatusOcorrenciaGreId,OcorrenciaGreId")] HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicoStatusOcorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude", historicoStatusOcorrenciaGre.OcorrenciaGreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", historicoStatusOcorrenciaGre.StatusOcorrenciaGreId);
            return View(historicoStatusOcorrenciaGre);
        }

        // GET: HistoricoStatusOcorrenciaGre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre = db.HistoricoStatusOcorrenciaGres.Find(id);
            if (historicoStatusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaGre);
        }

        // POST: HistoricoStatusOcorrenciaGre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre = db.HistoricoStatusOcorrenciaGres.Find(id);
            db.HistoricoStatusOcorrenciaGres.Remove(historicoStatusOcorrenciaGre);
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

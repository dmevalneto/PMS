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
    public class HistoricoStatusOcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: HistoricoStatusOcorrenciaPB
        public ActionResult Index()
        {
            var historicoStatusOcorrenciaPBs = db.HistoricoStatusOcorrenciaPBs.Include(h => h.OcorrenciaPB).Include(h => h.StatusOcorrenciaPB);
            return View(historicoStatusOcorrenciaPBs.ToList());
        }

        // GET: HistoricoStatusOcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB = db.HistoricoStatusOcorrenciaPBs.Find(id);
            if (historicoStatusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaPB);
        }

        // GET: HistoricoStatusOcorrenciaPB/Create
        public ActionResult Create()
        {
            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude");
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status");
            return View();
        }

        // POST: HistoricoStatusOcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoricoStatusOcorrenciaPBId,Data,Observacao,StatusOcorrenciaPBId,OcorrenciaPBId")] HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.HistoricoStatusOcorrenciaPBs.Add(historicoStatusOcorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude", historicoStatusOcorrenciaPB.OcorrenciaPBId);
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status", historicoStatusOcorrenciaPB.StatusOcorrenciaPBId);
            return View(historicoStatusOcorrenciaPB);
        }

        // GET: HistoricoStatusOcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB = db.HistoricoStatusOcorrenciaPBs.Find(id);
            if (historicoStatusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude", historicoStatusOcorrenciaPB.OcorrenciaPBId);
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status", historicoStatusOcorrenciaPB.StatusOcorrenciaPBId);
            return View(historicoStatusOcorrenciaPB);
        }

        // POST: HistoricoStatusOcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoricoStatusOcorrenciaPBId,Data,Observacao,StatusOcorrenciaPBId,OcorrenciaPBId")] HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicoStatusOcorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OcorrenciaPBId = new SelectList(db.OcorrenciaPBs, "OcorrenciaPBId", "latitude", historicoStatusOcorrenciaPB.OcorrenciaPBId);
            ViewBag.StatusOcorrenciaPBId = new SelectList(db.StatusOcorrenciaPBs, "StatusOcorrenciaPBId", "Status", historicoStatusOcorrenciaPB.StatusOcorrenciaPBId);
            return View(historicoStatusOcorrenciaPB);
        }

        // GET: HistoricoStatusOcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB = db.HistoricoStatusOcorrenciaPBs.Find(id);
            if (historicoStatusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(historicoStatusOcorrenciaPB);
        }

        // POST: HistoricoStatusOcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoricoStatusOcorrenciaPB historicoStatusOcorrenciaPB = db.HistoricoStatusOcorrenciaPBs.Find(id);
            db.HistoricoStatusOcorrenciaPBs.Remove(historicoStatusOcorrenciaPB);
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

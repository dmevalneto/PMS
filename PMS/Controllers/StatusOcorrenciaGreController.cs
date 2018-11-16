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
    public class StatusOcorrenciaGreController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: StatusOcorrenciaGre
        public ActionResult Index()
        {
            return View(db.StatusOcorrenciaGres.ToList());
        }

        // GET: StatusOcorrenciaGre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaGre statusOcorrenciaGre = db.StatusOcorrenciaGres.Find(id);
            if (statusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaGre);
        }

        // GET: StatusOcorrenciaGre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusOcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusOcorrenciaGreId,Status,Observacao")] StatusOcorrenciaGre statusOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.StatusOcorrenciaGres.Add(statusOcorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusOcorrenciaGre);
        }

        // GET: StatusOcorrenciaGre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaGre statusOcorrenciaGre = db.StatusOcorrenciaGres.Find(id);
            if (statusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaGre);
        }

        // POST: StatusOcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusOcorrenciaGreId,Status,Observacao")] StatusOcorrenciaGre statusOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusOcorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusOcorrenciaGre);
        }

        // GET: StatusOcorrenciaGre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaGre statusOcorrenciaGre = db.StatusOcorrenciaGres.Find(id);
            if (statusOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaGre);
        }

        // POST: StatusOcorrenciaGre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusOcorrenciaGre statusOcorrenciaGre = db.StatusOcorrenciaGres.Find(id);
            db.StatusOcorrenciaGres.Remove(statusOcorrenciaGre);
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

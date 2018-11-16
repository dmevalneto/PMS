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
    public class StatusOcorrenciaSecController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: StatusOcorrenciaSec
        public ActionResult Index()
        {
            return View(db.StatusOcorrenciaSecs.ToList());
        }

        // GET: StatusOcorrenciaSec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaSec statusOcorrenciaSec = db.StatusOcorrenciaSecs.Find(id);
            if (statusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaSec);
        }

        // GET: StatusOcorrenciaSec/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusOcorrenciaSec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusOcorrenciaSecId,Status,Observacao")] StatusOcorrenciaSec statusOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.StatusOcorrenciaSecs.Add(statusOcorrenciaSec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusOcorrenciaSec);
        }

        // GET: StatusOcorrenciaSec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaSec statusOcorrenciaSec = db.StatusOcorrenciaSecs.Find(id);
            if (statusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaSec);
        }

        // POST: StatusOcorrenciaSec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusOcorrenciaSecId,Status,Observacao")] StatusOcorrenciaSec statusOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusOcorrenciaSec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusOcorrenciaSec);
        }

        // GET: StatusOcorrenciaSec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaSec statusOcorrenciaSec = db.StatusOcorrenciaSecs.Find(id);
            if (statusOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaSec);
        }

        // POST: StatusOcorrenciaSec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusOcorrenciaSec statusOcorrenciaSec = db.StatusOcorrenciaSecs.Find(id);
            db.StatusOcorrenciaSecs.Remove(statusOcorrenciaSec);
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

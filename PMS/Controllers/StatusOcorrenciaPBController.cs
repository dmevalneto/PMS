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
    public class StatusOcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: StatusOcorrenciaPB
        public ActionResult Index()
        {
            return View(db.StatusOcorrenciaPBs.ToList());
        }

        // GET: StatusOcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaPB statusOcorrenciaPB = db.StatusOcorrenciaPBs.Find(id);
            if (statusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaPB);
        }

        // GET: StatusOcorrenciaPB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusOcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusOcorrenciaPBId,Status,Observacao")] StatusOcorrenciaPB statusOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.StatusOcorrenciaPBs.Add(statusOcorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusOcorrenciaPB);
        }

        // GET: StatusOcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaPB statusOcorrenciaPB = db.StatusOcorrenciaPBs.Find(id);
            if (statusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaPB);
        }

        // POST: StatusOcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusOcorrenciaPBId,Status,Observacao")] StatusOcorrenciaPB statusOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusOcorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusOcorrenciaPB);
        }

        // GET: StatusOcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaPB statusOcorrenciaPB = db.StatusOcorrenciaPBs.Find(id);
            if (statusOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaPB);
        }

        // POST: StatusOcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusOcorrenciaPB statusOcorrenciaPB = db.StatusOcorrenciaPBs.Find(id);
            db.StatusOcorrenciaPBs.Remove(statusOcorrenciaPB);
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

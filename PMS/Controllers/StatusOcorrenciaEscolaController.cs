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
    public class StatusOcorrenciaEscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: StatusOcorrenciaEscola
        public ActionResult Index()
        {
            return View(db.StatusOcorrenciaEscolas.ToList());
        }

        // GET: StatusOcorrenciaEscola/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaEscola statusOcorrenciaEscola = db.StatusOcorrenciaEscolas.Find(id);
            if (statusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaEscola);
        }

        // GET: StatusOcorrenciaEscola/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusOcorrenciaEscola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusOcorrenciaEscolaId,Status,Observacao")] StatusOcorrenciaEscola statusOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.StatusOcorrenciaEscolas.Add(statusOcorrenciaEscola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusOcorrenciaEscola);
        }

        // GET: StatusOcorrenciaEscola/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaEscola statusOcorrenciaEscola = db.StatusOcorrenciaEscolas.Find(id);
            if (statusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaEscola);
        }

        // POST: StatusOcorrenciaEscola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusOcorrenciaEscolaId,Status,Observacao")] StatusOcorrenciaEscola statusOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusOcorrenciaEscola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusOcorrenciaEscola);
        }

        // GET: StatusOcorrenciaEscola/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOcorrenciaEscola statusOcorrenciaEscola = db.StatusOcorrenciaEscolas.Find(id);
            if (statusOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(statusOcorrenciaEscola);
        }

        // POST: StatusOcorrenciaEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusOcorrenciaEscola statusOcorrenciaEscola = db.StatusOcorrenciaEscolas.Find(id);
            db.StatusOcorrenciaEscolas.Remove(statusOcorrenciaEscola);
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

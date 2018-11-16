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
    public class TipoOcorrenciaSecController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: TipoOcorrenciaSec
        public ActionResult Index()
        {
            return View(db.TipoOcorrenciaSecs.ToList());
        }

        // GET: TipoOcorrenciaSec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaSec tipoOcorrenciaSec = db.TipoOcorrenciaSecs.Find(id);
            if (tipoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaSec);
        }

        // GET: TipoOcorrenciaSec/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoOcorrenciaSec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoOcorrenciaSecId,Tipo,Observacao")] TipoOcorrenciaSec tipoOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.TipoOcorrenciaSecs.Add(tipoOcorrenciaSec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoOcorrenciaSec);
        }

        // GET: TipoOcorrenciaSec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaSec tipoOcorrenciaSec = db.TipoOcorrenciaSecs.Find(id);
            if (tipoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaSec);
        }

        // POST: TipoOcorrenciaSec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoOcorrenciaSecId,Tipo,Observacao")] TipoOcorrenciaSec tipoOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOcorrenciaSec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoOcorrenciaSec);
        }

        // GET: TipoOcorrenciaSec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaSec tipoOcorrenciaSec = db.TipoOcorrenciaSecs.Find(id);
            if (tipoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaSec);
        }

        // POST: TipoOcorrenciaSec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOcorrenciaSec tipoOcorrenciaSec = db.TipoOcorrenciaSecs.Find(id);
            db.TipoOcorrenciaSecs.Remove(tipoOcorrenciaSec);
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

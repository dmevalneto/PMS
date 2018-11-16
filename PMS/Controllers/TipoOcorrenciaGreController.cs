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
    public class TipoOcorrenciaGreController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: TipoOcorrenciaGre
        public ActionResult Index()
        {
            return View(db.TipoOcorrenciaGres.ToList());
        }

        // GET: TipoOcorrenciaGre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaGre tipoOcorrenciaGre = db.TipoOcorrenciaGres.Find(id);
            if (tipoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaGre);
        }

        // GET: TipoOcorrenciaGre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoOcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoOcorrenciaGreId,Tipo,Observacao")] TipoOcorrenciaGre tipoOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.TipoOcorrenciaGres.Add(tipoOcorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoOcorrenciaGre);
        }

        // GET: TipoOcorrenciaGre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaGre tipoOcorrenciaGre = db.TipoOcorrenciaGres.Find(id);
            if (tipoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaGre);
        }

        // POST: TipoOcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoOcorrenciaGreId,Tipo,Observacao")] TipoOcorrenciaGre tipoOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOcorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoOcorrenciaGre);
        }

        // GET: TipoOcorrenciaGre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaGre tipoOcorrenciaGre = db.TipoOcorrenciaGres.Find(id);
            if (tipoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaGre);
        }

        // POST: TipoOcorrenciaGre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOcorrenciaGre tipoOcorrenciaGre = db.TipoOcorrenciaGres.Find(id);
            db.TipoOcorrenciaGres.Remove(tipoOcorrenciaGre);
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

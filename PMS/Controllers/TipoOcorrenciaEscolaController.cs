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
    public class TipoOcorrenciaEscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: TipoOcorrenciaEscola
        public ActionResult Index()
        {
            return View(db.TipoOcorrenciaEscolas.ToList());
        }

        // GET: TipoOcorrenciaEscola/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaEscola tipoOcorrenciaEscola = db.TipoOcorrenciaEscolas.Find(id);
            if (tipoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaEscola);
        }

        // GET: TipoOcorrenciaEscola/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoOcorrenciaEscola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoOcorrenciaEscolaId,Tipo,Observacao")] TipoOcorrenciaEscola tipoOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.TipoOcorrenciaEscolas.Add(tipoOcorrenciaEscola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoOcorrenciaEscola);
        }

        // GET: TipoOcorrenciaEscola/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaEscola tipoOcorrenciaEscola = db.TipoOcorrenciaEscolas.Find(id);
            if (tipoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaEscola);
        }

        // POST: TipoOcorrenciaEscola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoOcorrenciaEscolaId,Tipo,Observacao")] TipoOcorrenciaEscola tipoOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOcorrenciaEscola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoOcorrenciaEscola);
        }

        // GET: TipoOcorrenciaEscola/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaEscola tipoOcorrenciaEscola = db.TipoOcorrenciaEscolas.Find(id);
            if (tipoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaEscola);
        }

        // POST: TipoOcorrenciaEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOcorrenciaEscola tipoOcorrenciaEscola = db.TipoOcorrenciaEscolas.Find(id);
            db.TipoOcorrenciaEscolas.Remove(tipoOcorrenciaEscola);
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

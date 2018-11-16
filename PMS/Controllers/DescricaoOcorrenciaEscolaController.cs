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
    public class DescricaoOcorrenciaEscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: DescricaoOcorrenciaEscola
        public ActionResult Index()
        {
            return View(db.DescricaoOcorrenciaEscolas.ToList());
        }

        // GET: DescricaoOcorrenciaEscola/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaEscola descricaoOcorrenciaEscola = db.DescricaoOcorrenciaEscolas.Find(id);
            if (descricaoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaEscola);
        }

        // GET: DescricaoOcorrenciaEscola/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DescricaoOcorrenciaEscola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DescricaoOcorrenciaEscolaId,Descricao,Observacao")] DescricaoOcorrenciaEscola descricaoOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.DescricaoOcorrenciaEscolas.Add(descricaoOcorrenciaEscola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descricaoOcorrenciaEscola);
        }

        // GET: DescricaoOcorrenciaEscola/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaEscola descricaoOcorrenciaEscola = db.DescricaoOcorrenciaEscolas.Find(id);
            if (descricaoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaEscola);
        }

        // POST: DescricaoOcorrenciaEscola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescricaoOcorrenciaEscolaId,Descricao,Observacao")] DescricaoOcorrenciaEscola descricaoOcorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descricaoOcorrenciaEscola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descricaoOcorrenciaEscola);
        }

        // GET: DescricaoOcorrenciaEscola/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaEscola descricaoOcorrenciaEscola = db.DescricaoOcorrenciaEscolas.Find(id);
            if (descricaoOcorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaEscola);
        }

        // POST: DescricaoOcorrenciaEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DescricaoOcorrenciaEscola descricaoOcorrenciaEscola = db.DescricaoOcorrenciaEscolas.Find(id);
            db.DescricaoOcorrenciaEscolas.Remove(descricaoOcorrenciaEscola);
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

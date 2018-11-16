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
    public class DescricaoOcorrenciaGreController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: DescricaoOcorrenciaGre
        public ActionResult Index()
        {
            return View(db.DescricaoOcorrenciaGres.ToList());
        }

        // GET: DescricaoOcorrenciaGre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaGre descricaoOcorrenciaGre = db.DescricaoOcorrenciaGres.Find(id);
            if (descricaoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaGre);
        }

        // GET: DescricaoOcorrenciaGre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DescricaoOcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DescricaoOcorrenciaGreId,Descricao,Observacao")] DescricaoOcorrenciaGre descricaoOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.DescricaoOcorrenciaGres.Add(descricaoOcorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descricaoOcorrenciaGre);
        }

        // GET: DescricaoOcorrenciaGre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaGre descricaoOcorrenciaGre = db.DescricaoOcorrenciaGres.Find(id);
            if (descricaoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaGre);
        }

        // POST: DescricaoOcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescricaoOcorrenciaGreId,Descricao,Observacao")] DescricaoOcorrenciaGre descricaoOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descricaoOcorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descricaoOcorrenciaGre);
        }

        // GET: DescricaoOcorrenciaGre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaGre descricaoOcorrenciaGre = db.DescricaoOcorrenciaGres.Find(id);
            if (descricaoOcorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaGre);
        }

        // POST: DescricaoOcorrenciaGre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DescricaoOcorrenciaGre descricaoOcorrenciaGre = db.DescricaoOcorrenciaGres.Find(id);
            db.DescricaoOcorrenciaGres.Remove(descricaoOcorrenciaGre);
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

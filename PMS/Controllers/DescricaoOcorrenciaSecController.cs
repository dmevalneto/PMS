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
    public class DescricaoOcorrenciaSecController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: DescricaoOcorrenciaSec
        public ActionResult Index()
        {
            return View(db.DescricaoOcorrenciaSecs.ToList());
        }

        // GET: DescricaoOcorrenciaSec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaSec descricaoOcorrenciaSec = db.DescricaoOcorrenciaSecs.Find(id);
            if (descricaoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaSec);
        }

        // GET: DescricaoOcorrenciaSec/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DescricaoOcorrenciaSec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DescricaoOcorrenciaSecId,Descricao,Observacao")] DescricaoOcorrenciaSec descricaoOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.DescricaoOcorrenciaSecs.Add(descricaoOcorrenciaSec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descricaoOcorrenciaSec);
        }

        // GET: DescricaoOcorrenciaSec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaSec descricaoOcorrenciaSec = db.DescricaoOcorrenciaSecs.Find(id);
            if (descricaoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaSec);
        }

        // POST: DescricaoOcorrenciaSec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescricaoOcorrenciaSecId,Descricao,Observacao")] DescricaoOcorrenciaSec descricaoOcorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descricaoOcorrenciaSec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descricaoOcorrenciaSec);
        }

        // GET: DescricaoOcorrenciaSec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaSec descricaoOcorrenciaSec = db.DescricaoOcorrenciaSecs.Find(id);
            if (descricaoOcorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaSec);
        }

        // POST: DescricaoOcorrenciaSec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DescricaoOcorrenciaSec descricaoOcorrenciaSec = db.DescricaoOcorrenciaSecs.Find(id);
            db.DescricaoOcorrenciaSecs.Remove(descricaoOcorrenciaSec);
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

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
    public class DescricaoOcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: DescricaoOcorrenciaPB
        public ActionResult Index()
        {
            return View(db.DescricaoOcorrenciaPBs.ToList());
        }

        // GET: DescricaoOcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaPB descricaoOcorrenciaPB = db.DescricaoOcorrenciaPBs.Find(id);
            if (descricaoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaPB);
        }

        // GET: DescricaoOcorrenciaPB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DescricaoOcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DescricaoOcorrenciaPBId,Descricao,Observacao")] DescricaoOcorrenciaPB descricaoOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.DescricaoOcorrenciaPBs.Add(descricaoOcorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descricaoOcorrenciaPB);
        }

        // GET: DescricaoOcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaPB descricaoOcorrenciaPB = db.DescricaoOcorrenciaPBs.Find(id);
            if (descricaoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaPB);
        }

        // POST: DescricaoOcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescricaoOcorrenciaPBId,Descricao,Observacao")] DescricaoOcorrenciaPB descricaoOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descricaoOcorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descricaoOcorrenciaPB);
        }

        // GET: DescricaoOcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescricaoOcorrenciaPB descricaoOcorrenciaPB = db.DescricaoOcorrenciaPBs.Find(id);
            if (descricaoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(descricaoOcorrenciaPB);
        }

        // POST: DescricaoOcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DescricaoOcorrenciaPB descricaoOcorrenciaPB = db.DescricaoOcorrenciaPBs.Find(id);
            db.DescricaoOcorrenciaPBs.Remove(descricaoOcorrenciaPB);
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

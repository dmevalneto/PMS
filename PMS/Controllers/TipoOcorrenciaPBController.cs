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
    public class TipoOcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: TipoOcorrenciaPB
        public ActionResult Index()
        {
            return View(db.TipoOcorrenciaPBs.ToList());
        }

        // GET: TipoOcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaPB tipoOcorrenciaPB = db.TipoOcorrenciaPBs.Find(id);
            if (tipoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaPB);
        }

        // GET: TipoOcorrenciaPB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoOcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoOcorrenciaPBId,Tipo,Observacao")] TipoOcorrenciaPB tipoOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.TipoOcorrenciaPBs.Add(tipoOcorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoOcorrenciaPB);
        }

        // GET: TipoOcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaPB tipoOcorrenciaPB = db.TipoOcorrenciaPBs.Find(id);
            if (tipoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaPB);
        }

        // POST: TipoOcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoOcorrenciaPBId,Tipo,Observacao")] TipoOcorrenciaPB tipoOcorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOcorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoOcorrenciaPB);
        }

        // GET: TipoOcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOcorrenciaPB tipoOcorrenciaPB = db.TipoOcorrenciaPBs.Find(id);
            if (tipoOcorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(tipoOcorrenciaPB);
        }

        // POST: TipoOcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOcorrenciaPB tipoOcorrenciaPB = db.TipoOcorrenciaPBs.Find(id);
            db.TipoOcorrenciaPBs.Remove(tipoOcorrenciaPB);
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

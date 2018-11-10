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
    public class TipoCargoController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.TipoCargoes.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCargo tipoCargo = db.TipoCargoes.Find(id);
            if (tipoCargo == null)
            {
                return HttpNotFound();
            }
            return View(tipoCargo);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "TipoCargoId,Nome")] TipoCargo tipoCargo)
        {
            if (ModelState.IsValid)
            {
                db.TipoCargoes.Add(tipoCargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCargo);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCargo tipoCargo = db.TipoCargoes.Find(id);
            if (tipoCargo == null)
            {
                return HttpNotFound();
            }
            return View(tipoCargo);
        }

        // POST: TipoCargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "TipoCargoId,Nome")] TipoCargo tipoCargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoCargo);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCargo tipoCargo = db.TipoCargoes.Find(id);
            if (tipoCargo == null)
            {
                return HttpNotFound();
            }
            return View(tipoCargo);
        }

        [Authorize(Roles = "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCargo tipoCargo = db.TipoCargoes.Find(id);
            db.TipoCargoes.Remove(tipoCargo);
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

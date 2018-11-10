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
    public class GreController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            var gres = db.Gres.Include(g => g.Secretaria);
            return View(gres.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gre gre = db.Gres.Find(id);
            if (gre == null)
            {
                return HttpNotFound();
            }
            return View(gre);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome");
            return View();
        }

        // POST: Gre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "GreId,Regional,Codigo,Endereco,Referencia,Cep,Email,Tel1,Tel2,Gestor,SecretariaId")] Gre gre)
        {
            if (ModelState.IsValid)
            {
                db.Gres.Add(gre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", gre.SecretariaId);
            return View(gre);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gre gre = db.Gres.Find(id);
            if (gre == null)
            {
                return HttpNotFound();
            }
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", gre.SecretariaId);
            return View(gre);
        }

        // POST: Gre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "GreId,Regional,Codigo,Endereco,Referencia,Cep,Email,Tel1,Tel2,Gestor,SecretariaId")] Gre gre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", gre.SecretariaId);
            return View(gre);
        }

        // GET: Gre/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gre gre = db.Gres.Find(id);
            if (gre == null)
            {
                return HttpNotFound();
            }
            return View(gre);
        }

        // POST: Gre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Gre gre = db.Gres.Find(id);
            db.Gres.Remove(gre);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                //throw;
            }
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

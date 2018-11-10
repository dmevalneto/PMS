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
   
    public class SecretariaController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "Secretaria")]
        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.Secretarias.ToList());
        }


        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secretaria secretaria = db.Secretarias.Find(id);
            if (secretaria == null)
            {
                return HttpNotFound();
            }
            return View(secretaria);
        }

        // GET: Secretaria/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secretaria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "SecretariaId,Nome,Setor,Observacao")] Secretaria secretaria)
        {
            if (ModelState.IsValid)
            {
                db.Secretarias.Add(secretaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secretaria);
        }

        // GET: Secretaria/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secretaria secretaria = db.Secretarias.Find(id);
            if (secretaria == null)
            {
                return HttpNotFound();
            }
            return View(secretaria);
        }

        // POST: Secretaria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "SecretariaId,Nome,Setor,Observacao")] Secretaria secretaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secretaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secretaria);
        }

        // GET: Secretaria/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secretaria secretaria = db.Secretarias.Find(id);
            if (secretaria == null)
            {
                return HttpNotFound();
            }
            return View(secretaria);
        }

        // POST: Secretaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Secretaria secretaria = db.Secretarias.Find(id);
            db.Secretarias.Remove(secretaria);
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

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
    public class PrefeituraBairroController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.PrefeituraBairroes.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefeituraBairro prefeituraBairro = db.PrefeituraBairroes.Find(id);
            if (prefeituraBairro == null)
            {
                return HttpNotFound();
            }
            return View(prefeituraBairro);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrefeituraBairro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "PrefeituraBairroId,Codigo,Nome,Endereco,Referencia,Cep,Email,Tel1,Tel2,Gestor")] PrefeituraBairro prefeituraBairro)
        {
            if (ModelState.IsValid)
            {
                db.PrefeituraBairroes.Add(prefeituraBairro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prefeituraBairro);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefeituraBairro prefeituraBairro = db.PrefeituraBairroes.Find(id);
            if (prefeituraBairro == null)
            {
                return HttpNotFound();
            }
            return View(prefeituraBairro);
        }

        // POST: PrefeituraBairro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "PrefeituraBairroId,Codigo,Nome,Endereco,Referencia,Cep,Email,Tel1,Tel2,Gestor")] PrefeituraBairro prefeituraBairro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prefeituraBairro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prefeituraBairro);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefeituraBairro prefeituraBairro = db.PrefeituraBairroes.Find(id);
            if (prefeituraBairro == null)
            {
                return HttpNotFound();
            }
            return View(prefeituraBairro);
        }

        // POST: PrefeituraBairro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrefeituraBairro prefeituraBairro = db.PrefeituraBairroes.Find(id);
            db.PrefeituraBairroes.Remove(prefeituraBairro);
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

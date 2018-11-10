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
    public class EscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            var escolas = db.Escolas.Include(e => e.Gre);
            return View(escolas.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escola escola = db.Escolas.Find(id);
            if (escola == null)
            {
                return HttpNotFound();
            }
            return View(escola);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional");
            return View();
        }

        // POST: Escola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "EscolaId,Codigo,Nome,Tel1,Tel2,Cep,Bairro,Logradouro,Numero,Latitude,Longitude,Gestor,GreId")] Escola escola)
        {
            if (ModelState.IsValid)
            {
                db.Escolas.Add(escola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", escola.GreId);
            return View(escola);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escola escola = db.Escolas.Find(id);
            if (escola == null)
            {
                return HttpNotFound();
            }
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", escola.GreId);
            return View(escola);
        }

        // POST: Escola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "EscolaId,Codigo,Nome,Tel1,Tel2,Cep,Bairro,Logradouro,Numero,Latitude,Longitude,Gestor,GreId")] Escola escola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", escola.GreId);
            return View(escola);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escola escola = db.Escolas.Find(id);
            if (escola == null)
            {
                return HttpNotFound();
            }
            return View(escola);
        }

        [Authorize(Roles = "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Escola escola = db.Escolas.Find(id);
            db.Escolas.Remove(escola);
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

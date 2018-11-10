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
    public class PrestadoraServicoController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.PrestadoraServicoes.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrestadoraServico prestadoraServico = db.PrestadoraServicoes.Find(id);
            if (prestadoraServico == null)
            {
                return HttpNotFound();
            }
            return View(prestadoraServico);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrestadoraServico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "PrestadoraServicoId,RazaoSocial,Cnpj,Telefone,Email")] PrestadoraServico prestadoraServico)
        {
            if (ModelState.IsValid)
            {
                db.PrestadoraServicoes.Add(prestadoraServico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prestadoraServico);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrestadoraServico prestadoraServico = db.PrestadoraServicoes.Find(id);
            if (prestadoraServico == null)
            {
                return HttpNotFound();
            }
            return View(prestadoraServico);
        }

        // POST: PrestadoraServico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "PrestadoraServicoId,RazaoSocial,Cnpj,Telefone,Email")] PrestadoraServico prestadoraServico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestadoraServico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prestadoraServico);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrestadoraServico prestadoraServico = db.PrestadoraServicoes.Find(id);
            if (prestadoraServico == null)
            {
                return HttpNotFound();
            }
            return View(prestadoraServico);
        }

        // POST: PrestadoraServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrestadoraServico prestadoraServico = db.PrestadoraServicoes.Find(id);
            db.PrestadoraServicoes.Remove(prestadoraServico);
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

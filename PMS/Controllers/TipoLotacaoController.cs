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
    [Authorize(Users = "dmevalneto@outlook.com")]
    public class TipoLotacaoController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.TipoLotacaos.ToList());
        }

        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLotacao tipoLotacao = db.TipoLotacaos.Find(id);
            if (tipoLotacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoLotacao);
        }

        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoLotacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        public ActionResult Create([Bind(Include = "TipoLotacaoId,Nome,Observacao")] TipoLotacao tipoLotacao)
        {
            if (ModelState.IsValid)
            {
                db.TipoLotacaos.Add(tipoLotacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoLotacao);
        }

        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLotacao tipoLotacao = db.TipoLotacaos.Find(id);
            if (tipoLotacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoLotacao);
        }

        // POST: TipoLotacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "TipoLotacaoId,Nome,Observacao")] TipoLotacao tipoLotacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoLotacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoLotacao);
        }

        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLotacao tipoLotacao = db.TipoLotacaos.Find(id);
            if (tipoLotacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoLotacao);
        }

        // POST: TipoLotacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoLotacao tipoLotacao = db.TipoLotacaos.Find(id);
            db.TipoLotacaos.Remove(tipoLotacao);
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

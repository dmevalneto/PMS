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

    public class CargoController : Controller
    {
        private PMSContext db = new PMSContext();

        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            ViewBag.Title = "Operações de Cargo";
            var cargoes = db.Cargoes.Include(c => c.TipoCargo);
            return View(cargoes.ToList());
        }
        

        [Authorize(Roles = "View")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        [Authorize(Roles = "Create")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.TipoCargoId = new SelectList(db.TipoCargoes, "TipoCargoId", "Nome");
            return View();
        }

        // POST: Cargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "CargoId,Nome,Descricao,TipoCargoId")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Cargoes.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoCargoId = new SelectList(db.TipoCargoes, "TipoCargoId", "Nome", cargo.TipoCargoId);
            return View(cargo);
        }

        [Authorize(Roles = "Edit")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoCargoId = new SelectList(db.TipoCargoes, "TipoCargoId", "Nome", cargo.TipoCargoId);
            return View(cargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "CargoId,Nome,Descricao,TipoCargoId")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoCargoId = new SelectList(db.TipoCargoes, "TipoCargoId", "Nome", cargo.TipoCargoId);
            return View(cargo);
        }

        [Authorize(Roles = "Delete")]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Delete")]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargoes.Find(id);
            db.Cargoes.Remove(cargo);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["TituloErro"] = "Desculpe";
                TempData["CorpoErro"] = "Você não tem permssão para excluir esse item. Por Favor contate o Administrador do sistema !!";
                return RedirectToAction("Erro");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Erro()
        {
            ViewBag.TituloErro = TempData["TituloErro"].ToString();
            ViewBag.CorpoErro = TempData["CorpoErro"].ToString();
            return View();
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

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
    public class OcorrenciaGreController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: OcorrenciaGre
        public ActionResult Index()
        {
            var ocorrenciaGres = db.OcorrenciaGres.Include(o => o.DescricaoOcorrenciaGre).Include(o => o.Gre).Include(o => o.StatusOcorrenciaGre).Include(o => o.TipoOcorrenciaGre);
            return View(ocorrenciaGres.ToList());
        }

        // GET: OcorrenciaGre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaGre ocorrenciaGre = db.OcorrenciaGres.Find(id);
            if (ocorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaGre);
        }

        // GET: OcorrenciaGre/Create
        public ActionResult Create()
        {
            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao");
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional");
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status");
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo");
            return View();
        }

        // POST: OcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaGreId,DescricaoOcorrenciaGreId,StatusOcorrenciaGreId,GreId")] OcorrenciaGre ocorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.OcorrenciaGres.Add(ocorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao", ocorrenciaGre.DescricaoOcorrenciaGreId);
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", ocorrenciaGre.GreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", ocorrenciaGre.StatusOcorrenciaGreId);
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo", ocorrenciaGre.TipoOcorrenciaGreId);
            return View(ocorrenciaGre);
        }

        // GET: OcorrenciaGre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaGre ocorrenciaGre = db.OcorrenciaGres.Find(id);
            if (ocorrenciaGre == null)
            {
                return HttpNotFound();
            }
            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao", ocorrenciaGre.DescricaoOcorrenciaGreId);
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", ocorrenciaGre.GreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", ocorrenciaGre.StatusOcorrenciaGreId);
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo", ocorrenciaGre.TipoOcorrenciaGreId);
            return View(ocorrenciaGre);
        }

        // POST: OcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaGreId,DescricaoOcorrenciaGreId,StatusOcorrenciaGreId,GreId")] OcorrenciaGre ocorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao", ocorrenciaGre.DescricaoOcorrenciaGreId);
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", ocorrenciaGre.GreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", ocorrenciaGre.StatusOcorrenciaGreId);
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo", ocorrenciaGre.TipoOcorrenciaGreId);
            return View(ocorrenciaGre);
        }

        // GET: OcorrenciaGre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaGre ocorrenciaGre = db.OcorrenciaGres.Find(id);
            if (ocorrenciaGre == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaGre);
        }

        // POST: OcorrenciaGre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcorrenciaGre ocorrenciaGre = db.OcorrenciaGres.Find(id);
            db.OcorrenciaGres.Remove(ocorrenciaGre);
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

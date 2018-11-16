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
    public class OcorrenciaSecController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: OcorrenciaSec
        public ActionResult Index()
        {
            var ocorrenciaSecs = db.OcorrenciaSecs.Include(o => o.DescricaoOcorrenciaSec).Include(o => o.Secretaria).Include(o => o.StatusOcorrenciaSec).Include(o => o.TipoOcorrenciaSec);
            return View(ocorrenciaSecs.ToList());
        }

        // GET: OcorrenciaSec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaSec ocorrenciaSec = db.OcorrenciaSecs.Find(id);
            if (ocorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaSec);
        }

        // GET: OcorrenciaSec/Create
        public ActionResult Create()
        {
            ViewBag.DescricaoOcorrenciaSecId = new SelectList(db.DescricaoOcorrenciaSecs, "DescricaoOcorrenciaSecId", "Descricao");
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome");
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status");
            ViewBag.TipoOcorrenciaSecId = new SelectList(db.TipoOcorrenciaSecs, "TipoOcorrenciaSecId", "Tipo");
            return View();
        }

        // POST: OcorrenciaSec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaSecId,DescricaoOcorrenciaSecId,StatusOcorrenciaSecId,SecretariaId")] OcorrenciaSec ocorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.OcorrenciaSecs.Add(ocorrenciaSec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaSecId = new SelectList(db.DescricaoOcorrenciaSecs, "DescricaoOcorrenciaSecId", "Descricao", ocorrenciaSec.DescricaoOcorrenciaSecId);
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", ocorrenciaSec.SecretariaId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", ocorrenciaSec.StatusOcorrenciaSecId);
            ViewBag.TipoOcorrenciaSecId = new SelectList(db.TipoOcorrenciaSecs, "TipoOcorrenciaSecId", "Tipo", ocorrenciaSec.TipoOcorrenciaSecId);
            return View(ocorrenciaSec);
        }

        // GET: OcorrenciaSec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaSec ocorrenciaSec = db.OcorrenciaSecs.Find(id);
            if (ocorrenciaSec == null)
            {
                return HttpNotFound();
            }
            ViewBag.DescricaoOcorrenciaSecId = new SelectList(db.DescricaoOcorrenciaSecs, "DescricaoOcorrenciaSecId", "Descricao", ocorrenciaSec.DescricaoOcorrenciaSecId);
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", ocorrenciaSec.SecretariaId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", ocorrenciaSec.StatusOcorrenciaSecId);
            ViewBag.TipoOcorrenciaSecId = new SelectList(db.TipoOcorrenciaSecs, "TipoOcorrenciaSecId", "Tipo", ocorrenciaSec.TipoOcorrenciaSecId);
            return View(ocorrenciaSec);
        }

        // POST: OcorrenciaSec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaSecId,DescricaoOcorrenciaSecId,StatusOcorrenciaSecId,SecretariaId")] OcorrenciaSec ocorrenciaSec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaSec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaSecId = new SelectList(db.DescricaoOcorrenciaSecs, "DescricaoOcorrenciaSecId", "Descricao", ocorrenciaSec.DescricaoOcorrenciaSecId);
            ViewBag.SecretariaId = new SelectList(db.Secretarias, "SecretariaId", "Nome", ocorrenciaSec.SecretariaId);
            ViewBag.StatusOcorrenciaSecId = new SelectList(db.StatusOcorrenciaSecs, "StatusOcorrenciaSecId", "Status", ocorrenciaSec.StatusOcorrenciaSecId);
            ViewBag.TipoOcorrenciaSecId = new SelectList(db.TipoOcorrenciaSecs, "TipoOcorrenciaSecId", "Tipo", ocorrenciaSec.TipoOcorrenciaSecId);
            return View(ocorrenciaSec);
        }

        // GET: OcorrenciaSec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaSec ocorrenciaSec = db.OcorrenciaSecs.Find(id);
            if (ocorrenciaSec == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaSec);
        }

        // POST: OcorrenciaSec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcorrenciaSec ocorrenciaSec = db.OcorrenciaSecs.Find(id);
            db.OcorrenciaSecs.Remove(ocorrenciaSec);
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

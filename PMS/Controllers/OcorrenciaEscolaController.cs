﻿using System;
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
    public class OcorrenciaEscolaController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: OcorrenciaEscola
        public ActionResult Index()
        {
            var ocorrenciaEscolas = db.OcorrenciaEscolas.Include(o => o.DescricaoOcorrenciaEscola).Include(o => o.Escola).Include(o => o.StatusOcorrenciaEscola).Include(o => o.TipoOcorrenciaEscola);
            return View(ocorrenciaEscolas.ToList());
        }

        // GET: OcorrenciaEscola/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaEscola ocorrenciaEscola = db.OcorrenciaEscolas.Find(id);
            if (ocorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaEscola);
        }

        // GET: OcorrenciaEscola/Create
        public ActionResult Create()
        {
            ViewBag.DescricaoOcorrenciaEscolaId = new SelectList(db.DescricaoOcorrenciaEscolas, "DescricaoOcorrenciaEscolaId", "Descricao");
            ViewBag.EscolaId = new SelectList(db.Escolas, "EscolaId", "Nome");
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status");
            ViewBag.TipoOcorrenciaEscolaId = new SelectList(db.TipoOcorrenciaEscolas, "TipoOcorrenciaEscolaId", "Tipo");
            return View();
        }

        // POST: OcorrenciaEscola/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaEscolaId,DescricaoOcorrenciaEscolaId,StatusOcorrenciaEscolaId,EscolaId")] OcorrenciaEscola ocorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.OcorrenciaEscolas.Add(ocorrenciaEscola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaEscolaId = new SelectList(db.DescricaoOcorrenciaEscolas, "DescricaoOcorrenciaEscolaId", "Descricao", ocorrenciaEscola.DescricaoOcorrenciaEscolaId);
            ViewBag.EscolaId = new SelectList(db.Escolas, "EscolaId", "Nome", ocorrenciaEscola.EscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", ocorrenciaEscola.StatusOcorrenciaEscolaId);
            ViewBag.TipoOcorrenciaEscolaId = new SelectList(db.TipoOcorrenciaEscolas, "TipoOcorrenciaEscolaId", "Tipo", ocorrenciaEscola.TipoOcorrenciaEscolaId);
            return View(ocorrenciaEscola);
        }

        // GET: OcorrenciaEscola/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaEscola ocorrenciaEscola = db.OcorrenciaEscolas.Find(id);
            if (ocorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            ViewBag.DescricaoOcorrenciaEscolaId = new SelectList(db.DescricaoOcorrenciaEscolas, "DescricaoOcorrenciaEscolaId", "Descricao", ocorrenciaEscola.DescricaoOcorrenciaEscolaId);
            ViewBag.EscolaId = new SelectList(db.Escolas, "EscolaId", "Nome", ocorrenciaEscola.EscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", ocorrenciaEscola.StatusOcorrenciaEscolaId);
            ViewBag.TipoOcorrenciaEscolaId = new SelectList(db.TipoOcorrenciaEscolas, "TipoOcorrenciaEscolaId", "Tipo", ocorrenciaEscola.TipoOcorrenciaEscolaId);
            return View(ocorrenciaEscola);
        }

        // POST: OcorrenciaEscola/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaSecId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaEscolaId,DescricaoOcorrenciaEscolaId,StatusOcorrenciaEscolaId,EscolaId")] OcorrenciaEscola ocorrenciaEscola)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaEscola).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaEscolaId = new SelectList(db.DescricaoOcorrenciaEscolas, "DescricaoOcorrenciaEscolaId", "Descricao", ocorrenciaEscola.DescricaoOcorrenciaEscolaId);
            ViewBag.EscolaId = new SelectList(db.Escolas, "EscolaId", "Nome", ocorrenciaEscola.EscolaId);
            ViewBag.StatusOcorrenciaEscolaId = new SelectList(db.StatusOcorrenciaEscolas, "StatusOcorrenciaEscolaId", "Status", ocorrenciaEscola.StatusOcorrenciaEscolaId);
            ViewBag.TipoOcorrenciaEscolaId = new SelectList(db.TipoOcorrenciaEscolas, "TipoOcorrenciaEscolaId", "Tipo", ocorrenciaEscola.TipoOcorrenciaEscolaId);
            return View(ocorrenciaEscola);
        }

        // GET: OcorrenciaEscola/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaEscola ocorrenciaEscola = db.OcorrenciaEscolas.Find(id);
            if (ocorrenciaEscola == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaEscola);
        }

        // POST: OcorrenciaEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcorrenciaEscola ocorrenciaEscola = db.OcorrenciaEscolas.Find(id);
            db.OcorrenciaEscolas.Remove(ocorrenciaEscola);
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

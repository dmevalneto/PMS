﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
            var ocorrenciaGres = db.OcorrenciaGres.Include(o => o.DescricaoOcorrenciaGre).Include(o => o.Gre).Include(o => o.TipoOcorrenciaGre);
            return View(ocorrenciaGres.ToList().OrderByDescending(d => d.Data));
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
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo");
            return View();
        }

        // POST: OcorrenciaGre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaGreId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaGreId,DescricaoOcorrenciaGreId,GreId")] OcorrenciaGre ocorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                ocorrenciaGre.Data = DateTime.Now;
                db.OcorrenciaGres.Add(ocorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao", ocorrenciaGre.DescricaoOcorrenciaGreId);
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", ocorrenciaGre.GreId);
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
            ViewBag.TipoOcorrenciaGreId = new SelectList(db.TipoOcorrenciaGres, "TipoOcorrenciaGreId", "Tipo", ocorrenciaGre.TipoOcorrenciaGreId);
            return View(ocorrenciaGre);
        }

        // POST: OcorrenciaGre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaGreId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,Data,TipoOcorrenciaGreId,DescricaoOcorrenciaGreId,GreId")] OcorrenciaGre ocorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaGre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaGreId = new SelectList(db.DescricaoOcorrenciaGres, "DescricaoOcorrenciaGreId", "Descricao", ocorrenciaGre.DescricaoOcorrenciaGreId);
            ViewBag.GreId = new SelectList(db.Gres, "GreId", "Regional", ocorrenciaGre.GreId);
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


        public ActionResult SalvarHistoricoStatus(int id)
        {
            ViewBag.id = id;
            TempData["id"] = id;
            TempData.Keep("id");
            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude");
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarHistoricoStatus([Bind(Include = "HistoricoStatusOcorrenciaGreId,Data,Observacao,StatusOcorrenciaGreId,OcorrenciaGreId")] HistoricoStatusOcorrenciaGre historicoStatusOcorrenciaGre)
        {
            if (ModelState.IsValid)
            {
                ViewBag.id = TempData["id"];
                historicoStatusOcorrenciaGre.Data = DateTime.Now;
                historicoStatusOcorrenciaGre.OcorrenciaGreId = ViewBag.id;
                db.HistoricoStatusOcorrenciaGres.Add(historicoStatusOcorrenciaGre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OcorrenciaGreId = new SelectList(db.OcorrenciaGres, "OcorrenciaGreId", "latitude", historicoStatusOcorrenciaGre.OcorrenciaGreId);
            ViewBag.StatusOcorrenciaGreId = new SelectList(db.StatusOcorrenciaGres, "StatusOcorrenciaGreId", "Status", historicoStatusOcorrenciaGre.StatusOcorrenciaGreId);
            return View(historicoStatusOcorrenciaGre);
        }

        public ActionResult HistoricoStatus(int id)
        {

            var hist = db.HistoricoStatusOcorrenciaGres.Where(c => c.OcorrenciaGreId == id);

            return View(hist);
        }

        public ActionResult ViewMapa(int id)
        {

            var ocorrenciaPBs = db.OcorrenciaGres.Where(x=> x.GreId == id).ToList();

            List<string> locations = new List<string>();
            double menor = 0;
            double maior = 0;
            var count = 0;

            foreach (var l in ocorrenciaPBs)
            {
                double latitude = Double.Parse(l.latitude, CultureInfo.InvariantCulture);
                double longitude = Double.Parse(l.longitude, CultureInfo.InvariantCulture);

                if (count == 0)
                {
                    menor = latitude;
                    maior = longitude;
                }

                if (latitude >= menor)
                {
                    menor = latitude;
                }

                if (longitude <= maior)
                {
                    maior = longitude;
                }

                var array = latitude.ToString().Replace(",", ".") + "&" + longitude.ToString().Replace(",", ".");
                locations.Add(array);
                count++;
            }

            ViewBag.Latitude = menor.ToString().Replace(",", ".");
            ViewBag.Longitude = maior.ToString().Replace(",", ".");
            ViewBag.Locations = locations;

            return View(ocorrenciaPBs);
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

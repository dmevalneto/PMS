using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMS.Models;

namespace PMS.Controllers
{
    public class OcorrenciaPBController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: OcorrenciaPB
        public ActionResult Index()
        {
            var ocorrenciaPBs = db.OcorrenciaPBs.Include(o => o.DescricaoOcorrenciaPB).Include(o => o.PrefeituraBairro).Include(o => o.TipoOcorrenciaPB);
            return View(ocorrenciaPBs.ToList());
        }

        // GET: OcorrenciaPB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaPB);
        }

        [Authorize(Roles = "PrefeituraBairro")]
        [Authorize(Roles = "View")]
        public ActionResult ListarPrefeituraBairro()
        {
            return View(db.PrefeituraBairroes.ToList());
        }

        [Authorize(Roles = "PrefeituraBairro")]
        [Authorize(Roles = "View")]
        public ActionResult SelecionarOcorrencia(int id)
        {
            return View(db.OcorrenciaPBs.ToList().Where(x => x.PrefeituraBairroId == id));
        }

        [Authorize(Roles = "PrefeituraBairro")]
        [Authorize(Roles = "View")]
        public ActionResult VisualizarOcorrencia()
        {
            var ocorrenciaPBs = db.OcorrenciaPBs.Include(o => o.DescricaoOcorrenciaPB).Include(o => o.PrefeituraBairro).Include(o => o.TipoOcorrenciaPB);
            return View(ocorrenciaPBs.ToList());
        }

        // GET: OcorrenciaPB/Create
        public ActionResult Create()
        {
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao");
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome");
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo");
            return View();
        }

        // POST: OcorrenciaPB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaPBId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,foto,Data,TipoOcorrenciaPBId,DescricaoOcorrenciaPBId,PrefeituraBairroId")] OcorrenciaPB ocorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                ocorrenciaPB.Data = DateTime.Now;
                db.OcorrenciaPBs.Add(ocorrenciaPB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // GET: OcorrenciaPB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // POST: OcorrenciaPB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaPBId,latitude,longitude,logradouro,numero,bairro,cep,cidade,estado,foto,Data,TipoOcorrenciaPBId,DescricaoOcorrenciaPBId,PrefeituraBairroId")] OcorrenciaPB ocorrenciaPB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrenciaPB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DescricaoOcorrenciaPBId = new SelectList(db.DescricaoOcorrenciaPBs, "DescricaoOcorrenciaPBId", "Descricao", ocorrenciaPB.DescricaoOcorrenciaPBId);
            ViewBag.PrefeituraBairroId = new SelectList(db.PrefeituraBairroes, "PrefeituraBairroId", "Nome", ocorrenciaPB.PrefeituraBairroId);
            ViewBag.TipoOcorrenciaPBId = new SelectList(db.TipoOcorrenciaPBs, "TipoOcorrenciaPBId", "Tipo", ocorrenciaPB.TipoOcorrenciaPBId);
            return View(ocorrenciaPB);
        }

        // GET: OcorrenciaPB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            if (ocorrenciaPB == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaPB);
        }

        // POST: OcorrenciaPB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcorrenciaPB ocorrenciaPB = db.OcorrenciaPBs.Find(id);
            db.OcorrenciaPBs.Remove(ocorrenciaPB);
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

        [ValidateAntiForgeryToken]
        [Route("upload-foto")]
        [HttpPost]
        public string UploadFoto(HttpPostedFileBase file)
        {
            var nomeArquivo = Guid.NewGuid().ToString() + ".jpg";
            var folder = Server.MapPath("~/UploadedFiles");
            string path = Path.Combine(folder, Path.GetFileName(nomeArquivo));

            if (file != null)
            {
                file.SaveAs(path);
            }

            return ("\\UploadedFiles\\" + nomeArquivo);
        }
    }
}

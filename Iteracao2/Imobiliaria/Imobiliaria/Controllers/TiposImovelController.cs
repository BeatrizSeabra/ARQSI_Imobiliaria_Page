using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Imobiliaria.DAL;
using Imobiliaria.Models;

namespace Imobiliaria.Controllers
{
    public class TiposImovelController : Controller
    {
        private ImobiliariaDBContext db = new ImobiliariaDBContext();

        // GET: TiposImovel
        public ActionResult Index()
        {
            return View(db.TiposImovel.ToList());
        }

        // GET: TiposImovel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TiposImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // GET: TiposImovel/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.pai = id;
            }
            else
            {
                ViewBag.pai = -1;
            }
            return View();
        }

        // POST: TiposImovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoImovelID,NomeTipo")] TipoImovel tipoImovel)
        {
            if (ModelState.IsValid)
            {
                db.TiposImovel.Add(tipoImovel);
                db.SaveChanges();
                try
                {
                    string str = Request["pai"];
                    int PaiId= int.Parse(str);
                    if (PaiId > 0)
                    {
                        TipoImovel TipoPai = db.TiposImovel.Find(PaiId);
                        if (TipoPai != null)
                        {
                            TipoPai.SubTipos.Add(tipoImovel);
                            db.Entry(tipoImovel).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception){}
                return RedirectToAction("Index");
            }

            return View(tipoImovel);
        }

        // GET: TiposImovel/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TiposImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // POST: TiposImovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoImovelID,NomeTipo")] TipoImovel tipoImovel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoImovel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoImovel);
        }

        // GET: TiposImovel/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TiposImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // POST: TiposImovel/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoImovel tipoImovel = db.TiposImovel.Find(id);
            db.TiposImovel.Remove(tipoImovel);
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

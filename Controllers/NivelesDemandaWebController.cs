using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicoreriaTom.Models;

namespace LicoreriaTom.Controllers
{
    public class NivelesDemandaWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: NivelesDemandaWeb
        public ActionResult Index()
        {
            return View(db.NivelDemanda.ToList());
        }

        // GET: NivelesDemandaWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDemanda nivelDemanda = db.NivelDemanda.Find(id);
            if (nivelDemanda == null)
            {
                return HttpNotFound();
            }
            return View(nivelDemanda);
        }

        // GET: NivelesDemandaWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelesDemandaWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,NivelProducto")] NivelDemanda nivelDemanda)
        {
            if (ModelState.IsValid)
            {
                db.NivelDemanda.Add(nivelDemanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelDemanda);
        }

        // GET: NivelesDemandaWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDemanda nivelDemanda = db.NivelDemanda.Find(id);
            if (nivelDemanda == null)
            {
                return HttpNotFound();
            }
            return View(nivelDemanda);
        }

        // POST: NivelesDemandaWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,NivelProducto")] NivelDemanda nivelDemanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivelDemanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelDemanda);
        }

        // GET: NivelesDemandaWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDemanda nivelDemanda = db.NivelDemanda.Find(id);
            if (nivelDemanda == null)
            {
                return HttpNotFound();
            }
            return View(nivelDemanda);
        }

        // POST: NivelesDemandaWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NivelDemanda nivelDemanda = db.NivelDemanda.Find(id);
            db.NivelDemanda.Remove(nivelDemanda);
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

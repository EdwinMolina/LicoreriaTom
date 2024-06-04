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
    public class FinanciamientosWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: FinanciamientosWeb
        public ActionResult Index()
        {
            return View(db.Financiamiento.ToList());
        }

        // GET: FinanciamientosWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiamiento financiamiento = db.Financiamiento.Find(id);
            if (financiamiento == null)
            {
                return HttpNotFound();
            }
            return View(financiamiento);
        }

        // GET: FinanciamientosWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinanciamientosWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,TipoFinanciamiento")] Financiamiento financiamiento)
        {
            if (ModelState.IsValid)
            {
                db.Financiamiento.Add(financiamiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(financiamiento);
        }

        // GET: FinanciamientosWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiamiento financiamiento = db.Financiamiento.Find(id);
            if (financiamiento == null)
            {
                return HttpNotFound();
            }
            return View(financiamiento);
        }

        // POST: FinanciamientosWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,TipoFinanciamiento")] Financiamiento financiamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financiamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financiamiento);
        }

        // GET: FinanciamientosWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financiamiento financiamiento = db.Financiamiento.Find(id);
            if (financiamiento == null)
            {
                return HttpNotFound();
            }
            return View(financiamiento);
        }

        // POST: FinanciamientosWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Financiamiento financiamiento = db.Financiamiento.Find(id);
            db.Financiamiento.Remove(financiamiento);
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

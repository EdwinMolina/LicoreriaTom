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
    public class TiposProductoWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: TiposProductoWeb
        public ActionResult Index()
        {
            return View(db.TipoProducto.ToList());
        }

        // GET: TiposProductoWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProducto tipoProducto = db.TipoProducto.Find(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // GET: TiposProductoWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposProductoWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Descripcion")] TipoProducto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                db.TipoProducto.Add(tipoProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoProducto);
        }

        // GET: TiposProductoWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProducto tipoProducto = db.TipoProducto.Find(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // POST: TiposProductoWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Descripcion")] TipoProducto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoProducto);
        }

        // GET: TiposProductoWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProducto tipoProducto = db.TipoProducto.Find(id);
            if (tipoProducto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProducto);
        }

        // POST: TiposProductoWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoProducto tipoProducto = db.TipoProducto.Find(id);
            db.TipoProducto.Remove(tipoProducto);
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

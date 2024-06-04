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
    public class DetallesVentaWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: DetallesVentaWeb
        public ActionResult Index()
        {
            var detalleVenta = db.DetalleVenta.Include(d => d.Financiamiento).Include(d => d.Producto).Include(d => d.Venta);
            return View(detalleVenta.ToList());
        }

        // GET: DetallesVentaWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            return View(detalleVenta);
        }

        // GET: DetallesVentaWeb/Create
        public ActionResult Create()
        {
            var financiamientos = db.Financiamiento.ToList();
            financiamientos.Insert(0, new Financiamiento { Id = 0, TipoFinanciamiento = "--Seleccione un financiamiento--" });
            ViewBag.Financiamientos = new SelectList(financiamientos, "Id", "TipoFinanciamiento");

            var productos = db.Producto.ToList();
            productos.Insert(0, new Producto { Id = 0, Nombre = "--Seleccione un producto--" });
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            ViewBag.VentaId = new SelectList(db.Venta, "Id", "Codigo");
            return View();
        }

        // POST: DetallesVentaWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Cantidad,Total,FinanciamientoId,ProductoId,VentaId")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                db.DetalleVenta.Add(detalleVenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FinanciamientoId = new SelectList(db.Financiamiento, "Id", "Codigo", detalleVenta.FinanciamientoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleVenta.ProductoId);
            ViewBag.VentaId = new SelectList(db.Venta, "Id", "Codigo", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetallesVentaWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.FinanciamientoId = new SelectList(db.Financiamiento, "Id", "Codigo", detalleVenta.FinanciamientoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleVenta.ProductoId);
            ViewBag.VentaId = new SelectList(db.Venta, "Id", "Codigo", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // POST: DetallesVentaWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Cantidad,Total,FinanciamientoId,ProductoId,VentaId")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleVenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FinanciamientoId = new SelectList(db.Financiamiento, "Id", "Codigo", detalleVenta.FinanciamientoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleVenta.ProductoId);
            ViewBag.VentaId = new SelectList(db.Venta, "Id", "Codigo", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetallesVentaWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            return View(detalleVenta);
        }

        // POST: DetallesVentaWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            db.DetalleVenta.Remove(detalleVenta);
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

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
    public class DetallesCompraWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: DetallesCompraWeb
        public ActionResult Index()
        {
            var detalleCompra = db.DetalleCompra.Include(d => d.Compra).Include(d => d.Producto);
            return View(detalleCompra.ToList());
        }

        // GET: DetallesCompraWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // GET: DetallesCompraWeb/Create
        public ActionResult Create()
        {
            var compras = db.Compra.ToList();
            compras.Insert(0, new Compra { Id = 0, Codigo = "--Seleccione una compra--" });
            ViewBag.Compras = new SelectList(compras, "Id", "Codigo");

            var productos = db.Producto.ToList();
            productos.Insert(0, new Producto { Id = 0, Nombre = "--Seleccione un producto--" });
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return View();
        }

        // POST: DetallesCompraWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Cantidad,Total,CompraId,ProductoId")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.DetalleCompra.Add(detalleCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompraId = new SelectList(db.Compra, "Id", "Codigo", detalleCompra.CompraId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // GET: DetallesCompraWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompraId = new SelectList(db.Compra, "Id", "Codigo", detalleCompra.CompraId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // POST: DetallesCompraWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Cantidad,Total,CompraId,ProductoId")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompraId = new SelectList(db.Compra, "Id", "Codigo", detalleCompra.CompraId);
            ViewBag.ProductoId = new SelectList(db.Producto, "Id", "Codigo", detalleCompra.ProductoId);
            return View(detalleCompra);
        }

        // GET: DetallesCompraWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // POST: DetallesCompraWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            db.DetalleCompra.Remove(detalleCompra);
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

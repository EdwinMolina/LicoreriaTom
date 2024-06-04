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
    public class ProductosWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: ProductosWeb
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.NivelDemanda).Include(p => p.TipoProducto);
            return View(producto.ToList());
        }

        // GET: ProductosWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: ProductosWeb/Create
        
        public ActionResult Create()
        {
            var tiposdeproducto = db.TipoProducto.ToList();
            tiposdeproducto.Insert(0, new TipoProducto { Id = 0, Descripcion = "--Seleccione un tipo de producto--" });
            ViewBag.TiposProducto = new SelectList(tiposdeproducto, "Id", "Descripcion");

            var nivelesdedemanda = db.NivelDemanda.ToList();
            nivelesdedemanda.Insert(0, new NivelDemanda { Id = 0, NivelProducto = "--Seleccione un nivel de demanda--" });
            ViewBag.NivelesDemanda = new SelectList(nivelesdedemanda, "Id", "NivelProducto");

            var categorias = db.Categoria.ToList();
            categorias.Insert(0, new Categoria { Id = 0, Nombre = "--Seleccione una categoria--" });
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

            return View();
        }

        // POST: ProductosWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Nombre,Precio,Existencias,TipoProductoId,NivelDemandaId,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categoria, "Id", "Codigo", producto.CategoriaId);
            ViewBag.NivelDemandaId = new SelectList(db.NivelDemanda, "Id", "Codigo", producto.NivelDemandaId);
            ViewBag.TipoProductoId = new SelectList(db.TipoProducto, "Id", "Codigo", producto.TipoProductoId);
            return View(producto);
        }

        // GET: ProductosWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "Id", "Codigo", producto.CategoriaId);
            ViewBag.NivelDemandaId = new SelectList(db.NivelDemanda, "Id", "Codigo", producto.NivelDemandaId);
            ViewBag.TipoProductoId = new SelectList(db.TipoProducto, "Id", "Codigo", producto.TipoProductoId);
            return View(producto);
        }

        // POST: ProductosWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Nombre,Precio,Existencias,TipoProductoId,NivelDemandaId,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "Id", "Codigo", producto.CategoriaId);
            ViewBag.NivelDemandaId = new SelectList(db.NivelDemanda, "Id", "Codigo", producto.NivelDemandaId);
            ViewBag.TipoProductoId = new SelectList(db.TipoProducto, "Id", "Codigo", producto.TipoProductoId);
            return View(producto);
        }

        // GET: ProductosWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: ProductosWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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

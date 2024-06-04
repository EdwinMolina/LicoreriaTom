using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicoreriaTom.DTOs;
using LicoreriaTom.Models;

namespace LicoreriaTom.Controllers
{
    public class ComprasWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: ComprasWeb
        public ActionResult Index()
        {
            var compra = db.Compra.Include(c => c.Trabajador);
            return View(compra.ToList());
        }

        // GET: ComprasWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: ComprasWeb/Create
        public ActionResult Create()
        {
            var trabajadores = db.Trabajador.Select(t => new TrabajadorDTO
            {
                Id = t.Id,
                Codigo = t.Codigo,
                NombreCompleto = t.Nombre + " " + t.Apellido,
                Telefono = t.Telefono
            }).ToList();
            trabajadores.Insert(0, new TrabajadorDTO { Id = 0, NombreCompleto = "--Seleccione un trabajador--" });
            ViewBag.Trabajadores = new SelectList(trabajadores, "Id", "NombreCompleto");

            return View();
        }

        // POST: ComprasWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Fecha,TrabajadorId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compra.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", compra.TrabajadorId);
            return View(compra);
        }

        // GET: ComprasWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", compra.TrabajadorId);
            return View(compra);
        }

        // POST: ComprasWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Fecha,TrabajadorId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", compra.TrabajadorId);
            return View(compra);
        }

        // GET: ComprasWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: ComprasWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compra.Find(id);
            db.Compra.Remove(compra);
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

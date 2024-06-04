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
    public class VentasWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: VentasWeb
        public ActionResult Index()
        {
            var venta = db.Venta.Include(v => v.Cliente).Include(v => v.Trabajador);
            return View(venta.ToList());
        }

        // GET: VentasWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: VentasWeb/Create
        public ActionResult Create()
        {
            //LISTA DE CLIENTES
            var clientes = db.Cliente.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreCompleto = c.Nombre + " " + c.Apellido
            }).ToList();
            clientes.Insert(0, new ClienteDTO { Id = 0, NombreCompleto = "--Seleccione un cliente--" });
            ViewBag.Clientes = new SelectList(clientes, "Id", "NombreCompleto");


            //LISTA DE TRABAJADORES
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

        // POST: VentasWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Fecha,ClienteId,TrabajadorId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Cliente, "Id", "Codigo", venta.ClienteId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", venta.TrabajadorId);
            return View(venta);
        }

        // GET: VentasWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "Id", "Codigo", venta.ClienteId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", venta.TrabajadorId);
            return View(venta);
        }

        // POST: VentasWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Fecha,ClienteId,TrabajadorId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "Id", "Codigo", venta.ClienteId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", venta.TrabajadorId);
            return View(venta);
        }

        // GET: VentasWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: VentasWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);
            db.Venta.Remove(venta);
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

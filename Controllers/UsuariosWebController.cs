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
    public class UsuariosWebController : Controller
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: UsuariosWeb
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Rol).Include(u => u.Trabajador);
            return View(usuario.ToList());
        }

        // GET: UsuariosWeb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: UsuariosWeb/Create
        public ActionResult Create()
        {
            var roles = db.Rol.ToList();
            roles.Insert(0, new Rol { Id = 0, TipoRol = "--Seleccione un rol--" });
            ViewBag.Roles = new SelectList(roles, "Id", "TipoRol");


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

        // POST: UsuariosWeb/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User,Contraseña,RolId,TrabajadorId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rol, "Id", "Codigo", usuario.RolId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", usuario.TrabajadorId);
            return View(usuario);
        }

        // GET: UsuariosWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Rol, "Id", "Codigo", usuario.RolId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", usuario.TrabajadorId);
            return View(usuario);
        }

        // POST: UsuariosWeb/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User,Contraseña,RolId,TrabajadorId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Rol, "Id", "Codigo", usuario.RolId);
            ViewBag.TrabajadorId = new SelectList(db.Trabajador, "Id", "Codigo", usuario.TrabajadorId);
            return View(usuario);
        }

        // GET: UsuariosWeb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: UsuariosWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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

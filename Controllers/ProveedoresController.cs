using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LicoreriaTom.DTOs;
using LicoreriaTom.Models;

namespace LicoreriaTom.Controllers
{
    public class ProveedoresController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Proveedores
        public IHttpActionResult GetProveedor()
        {
            var proveedores = db.Proveedor.Select(p => new ProveedorDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
            }).ToList();

            return Ok(proveedores);
        }

        // GET: api/Proveedores/5
        [ResponseType(typeof(Proveedor))]
        public IHttpActionResult GetProveedor(int id)
        {
            var proveedor = db.Proveedor.Where(p => p.Id == id).Select(p => new ProveedorDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
            }).FirstOrDefault();

            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }

        // PUT: api/Proveedores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProveedor(int id, Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proveedor.Id)
            {
                return BadRequest();
            }

            db.Entry(proveedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Proveedores
        [ResponseType(typeof(Proveedor))]
        public IHttpActionResult PostProveedor(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Proveedor.Add(proveedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = proveedor.Id }, proveedor);
        }

        // DELETE: api/Proveedores/5
        [ResponseType(typeof(Proveedor))]
        public IHttpActionResult DeleteProveedor(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            db.Proveedor.Remove(proveedor);
            db.SaveChanges();

            return Ok(proveedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProveedorExists(int id)
        {
            return db.Proveedor.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class ProductosController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Productos
        public IHttpActionResult GetProducto()
        {
            var productos = db.Producto.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Precio = p.Precio,
                TipoProductoId = p.TipoProductoId,
                CategoriaId = p.CategoriaId
            }).ToList();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(int id)
        {
            var producto = db.Producto.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Precio = p.Precio,
                TipoProductoId = p.TipoProductoId,
                CategoriaId = p.CategoriaId
            }).FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.Id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productos
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producto.Add(producto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Producto.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Producto.Count(e => e.Id == id) > 0;
        }
    }
}
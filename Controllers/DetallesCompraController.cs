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
    public class DetallesCompraController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/DetallesCompra
        public IHttpActionResult GetDetalleCompra()
        {
            var detallesCompra = db.DetalleCompra.Select(d => new DetalleCompraDTO
            {
                Id = d.Id,
                Codigo = d.Codigo,
                Cantidad = d.Cantidad,
                Total = d.Total,
                CompraId = d.CompraId,
                ProductoId = d.ProductoId
            });
            return Ok(detallesCompra);
        }

        // GET: api/DetallesCompra/5
        [ResponseType(typeof(DetalleCompra))]
        public IHttpActionResult GetDetalleCompra(int id)
        {
            var detalleCompra = db.DetalleCompra.Where(d => d.Id == id).Select(d => new DetalleCompraDTO
            {
                Id = d.Id,
                Codigo = d.Codigo,
                Cantidad = d.Cantidad,
                Total = d.Total,
                CompraId = d.CompraId,
                ProductoId = d.ProductoId
            }).FirstOrDefault();

            if (detalleCompra == null)
            {
                return NotFound();
            }

            return Ok(detalleCompra);
        }

        // PUT: api/DetallesCompra/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleCompra(int id, DetalleCompra detalleCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleCompra.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleCompra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleCompraExists(id))
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

        // POST: api/DetallesCompra
        [ResponseType(typeof(DetalleCompra))]
        public IHttpActionResult PostDetalleCompra(DetalleCompra detalleCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleCompra.Add(detalleCompra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleCompra.Id }, detalleCompra);
        }

        // DELETE: api/DetallesCompra/5
        [ResponseType(typeof(DetalleCompra))]
        public IHttpActionResult DeleteDetalleCompra(int id)
        {
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            db.DetalleCompra.Remove(detalleCompra);
            db.SaveChanges();

            return Ok(detalleCompra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleCompraExists(int id)
        {
            return db.DetalleCompra.Count(e => e.Id == id) > 0;
        }
    }
}
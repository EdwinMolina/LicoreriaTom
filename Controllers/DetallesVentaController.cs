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
    public class DetallesVentaController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/DetallesVenta
        public IHttpActionResult GetDetalleVenta()
        {
            var detallesVenta = db.DetalleVenta.Select(d => new DetalleVentaDTO
            {
                Id = d.Id,
                Codigo = d.Codigo,
                Cantidad = d.Cantidad,
                Total = d.Total,
                FinanciamientoId = d.FinanciamientoId,
                ProductoId = d.ProductoId,
                VentaId = d.VentaId
            });
            return Ok(detallesVenta);
        }

        // GET: api/DetallesVenta/5
        [ResponseType(typeof(DetalleVenta))]
        public IHttpActionResult GetDetalleVenta(int id)
        {
            var detalleVenta = db.DetalleVenta.Where(d => d.Id == id).Select(d => new DetalleVentaDTO
            {
                Id = d.Id,
                Codigo = d.Codigo,
                Cantidad = d.Cantidad,
                Total = d.Total,
                FinanciamientoId = d.FinanciamientoId,
                ProductoId = d.ProductoId,
                VentaId = d.VentaId
            }).FirstOrDefault();

            if (detalleVenta == null)
            {
                return NotFound();
            }

            return Ok(detalleVenta);
        }

        // PUT: api/DetallesVenta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleVenta(int id, DetalleVenta detalleVenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleVenta.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleVenta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaExists(id))
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

        // POST: api/DetallesVenta
        [ResponseType(typeof(DetalleVenta))]
        public IHttpActionResult PostDetalleVenta(DetalleVenta detalleVenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleVenta.Add(detalleVenta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleVenta.Id }, detalleVenta);
        }

        // DELETE: api/DetallesVenta/5
        [ResponseType(typeof(DetalleVenta))]
        public IHttpActionResult DeleteDetalleVenta(int id)
        {
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            db.DetalleVenta.Remove(detalleVenta);
            db.SaveChanges();

            return Ok(detalleVenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleVentaExists(int id)
        {
            return db.DetalleVenta.Count(e => e.Id == id) > 0;
        }
    }
}
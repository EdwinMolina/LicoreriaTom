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
    public class ComprasController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Compras
        public IHttpActionResult GetCompra()
        {
            var compras = db.Compra.Select(c => new CompraDTO
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Fecha = c.Fecha,
                TrabajadorId = c.TrabajadorId,
            }).ToList();

            return Ok(compras);
        }

        // GET: api/Compras/5
        [ResponseType(typeof(Compra))]
        public IHttpActionResult GetCompra(int id)
        {
            var compra = db.Compra.Where(c => c.Id == id).Select(c => new CompraDTO
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Fecha = c.Fecha,
                TrabajadorId = c.TrabajadorId,
            }).FirstOrDefault();

            if (compra == null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        // PUT: api/Compras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompra(int id, Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compra.Id)
            {
                return BadRequest();
            }

            db.Entry(compra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compras
        [ResponseType(typeof(Compra))]
        public IHttpActionResult PostCompra(Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Compra.Add(compra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = compra.Id }, compra);
        }

        // DELETE: api/Compras/5
        [ResponseType(typeof(Compra))]
        public IHttpActionResult DeleteCompra(int id)
        {
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return NotFound();
            }

            db.Compra.Remove(compra);
            db.SaveChanges();

            return Ok(compra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompraExists(int id)
        {
            return db.Compra.Count(e => e.Id == id) > 0;
        }
    }
}
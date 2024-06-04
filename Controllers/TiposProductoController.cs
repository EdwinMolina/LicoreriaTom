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
    public class TiposProductoController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/TiposProducto
        public IHttpActionResult GetTipoProducto()
        {
            var tiposProductos = db.TipoProducto.Select(tp => new TipoProductoDTO
            {
                Id = tp.Id,
                Codigo = tp.Codigo,
                Descripcion = tp.Descripcion
            }).ToList();
            return Ok(tiposProductos);
        }

        // GET: api/TiposProducto/5
        [ResponseType(typeof(TipoProducto))]
        public IHttpActionResult GetTipoProducto(int id)
        {
            var tipoProducto = db.TipoProducto.Where(tp => tp.Id == id).Select(tp => new TipoProductoDTO
            {
                Id = tp.Id,
                Codigo = tp.Codigo,
                Descripcion = tp.Descripcion
            }).FirstOrDefault();

            if (tipoProducto == null)
            {
                return NotFound();
            }

            return Ok(tipoProducto);
        }

        // PUT: api/TiposProducto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoProducto(int id, TipoProducto tipoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoProducto.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoProducto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProductoExists(id))
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

        // POST: api/TiposProducto
        [ResponseType(typeof(TipoProducto))]
        public IHttpActionResult PostTipoProducto(TipoProducto tipoProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoProducto.Add(tipoProducto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoProducto.Id }, tipoProducto);
        }

        // DELETE: api/TiposProducto/5
        [ResponseType(typeof(TipoProducto))]
        public IHttpActionResult DeleteTipoProducto(int id)
        {
            TipoProducto tipoProducto = db.TipoProducto.Find(id);
            if (tipoProducto == null)
            {
                return NotFound();
            }

            db.TipoProducto.Remove(tipoProducto);
            db.SaveChanges();

            return Ok(tipoProducto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoProductoExists(int id)
        {
            return db.TipoProducto.Count(e => e.Id == id) > 0;
        }
    }
}
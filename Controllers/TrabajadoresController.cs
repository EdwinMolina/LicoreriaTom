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
    public class TrabajadoresController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Trabajadores
        public IHttpActionResult GetTrabajador()
        {
            var trabajadores = db.Trabajador.Select(t => new TrabajadorDTO
            {
                Id = t.Id,
                Codigo = t.Codigo,
                NombreCompleto = t.Nombre + " " + t.Apellido,
                Telefono = t.Telefono,
            }).ToList();

            return Ok(trabajadores);
        }

        // GET: api/Trabajadores/5
        [ResponseType(typeof(Trabajador))]
        public IHttpActionResult GetTrabajador(int id)
        {
            var trabajador = db.Trabajador.Where(t => t.Id == id).Select(t => new TrabajadorDTO
            {
                Id = t.Id,
                Codigo = t.Codigo,
                NombreCompleto = t.Nombre + " " + t.Apellido,
                Telefono = t.Telefono,
            }).FirstOrDefault();

            if (trabajador == null)
            {
                return NotFound();
            }

            return Ok(trabajador);
        }

        // PUT: api/Trabajadores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrabajador(int id, Trabajador trabajador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trabajador.Id)
            {
                return BadRequest();
            }

            db.Entry(trabajador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajadorExists(id))
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

        // POST: api/Trabajadores
        [ResponseType(typeof(Trabajador))]
        public IHttpActionResult PostTrabajador(Trabajador trabajador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trabajador.Add(trabajador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trabajador.Id }, trabajador);
        }

        // DELETE: api/Trabajadores/5
        [ResponseType(typeof(Trabajador))]
        public IHttpActionResult DeleteTrabajador(int id)
        {
            Trabajador trabajador = db.Trabajador.Find(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            db.Trabajador.Remove(trabajador);
            db.SaveChanges();

            return Ok(trabajador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrabajadorExists(int id)
        {
            return db.Trabajador.Count(e => e.Id == id) > 0;
        }
    }
}
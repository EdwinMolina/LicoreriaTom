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
    public class FinanciamientosController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Financiamientos
        public IHttpActionResult GetFinanciamiento()
        {
            var financiamientos = db.Financiamiento.Select(f => new FinanciamientoDTO
            {
                Id = f.Id,
                Codigo = f.Codigo,
                TipoFinanciamiento = f.TipoFinanciamiento,
            }).ToList();

            return Ok(financiamientos);
        }

        // GET: api/Financiamientos/5
        [ResponseType(typeof(Financiamiento))]
        public IHttpActionResult GetFinanciamiento(int id)
        {
            var financiamiento = db.Financiamiento.Where(f => f.Id == id).Select(f => new FinanciamientoDTO
            {
                Id = f.Id,
                Codigo = f.Codigo,
                TipoFinanciamiento = f.TipoFinanciamiento,
            }).FirstOrDefault();

            if (financiamiento == null)
            {
                return NotFound();
            }

            return Ok(financiamiento);
        }

        // PUT: api/Financiamientos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFinanciamiento(int id, Financiamiento financiamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != financiamiento.Id)
            {
                return BadRequest();
            }

            db.Entry(financiamiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinanciamientoExists(id))
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

        // POST: api/Financiamientos
        [ResponseType(typeof(Financiamiento))]
        public IHttpActionResult PostFinanciamiento(Financiamiento financiamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Financiamiento.Add(financiamiento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = financiamiento.Id }, financiamiento);
        }

        // DELETE: api/Financiamientos/5
        [ResponseType(typeof(Financiamiento))]
        public IHttpActionResult DeleteFinanciamiento(int id)
        {
            Financiamiento financiamiento = db.Financiamiento.Find(id);
            if (financiamiento == null)
            {
                return NotFound();
            }

            db.Financiamiento.Remove(financiamiento);
            db.SaveChanges();

            return Ok(financiamiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinanciamientoExists(int id)
        {
            return db.Financiamiento.Count(e => e.Id == id) > 0;
        }
    }
}
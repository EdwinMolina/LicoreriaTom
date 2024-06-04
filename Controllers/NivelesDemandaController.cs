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
    public class NivelesDemandaController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/NivelesDemanda
        public IHttpActionResult GetNivelDemanda()
        {
            var nivelesDemanda = db.NivelDemanda.Select(nd => new NivelDemandaDTO
            {
                Id = nd.Id,
                Codigo = nd.Codigo,
                NivelProducto = nd.NivelProducto
            }).ToList();

            return Ok(nivelesDemanda);
        }

        // GET: api/NivelesDemanda/5
        [ResponseType(typeof(NivelDemanda))]
        public IHttpActionResult GetNivelDemanda(int id)
        {
            var nivelDemanda =  db.NivelDemanda.Where( nd => nd.Id == id).Select(nd => new NivelDemandaDTO
            {
                Id = nd.Id,
                Codigo = nd.Codigo,
                NivelProducto = nd.NivelProducto
            }).FirstOrDefault();
            
            if (nivelDemanda == null)
            {
                return NotFound();
            }

            return Ok(nivelDemanda);
        }

        // PUT: api/NivelesDemanda/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNivelDemanda(int id, NivelDemanda nivelDemanda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivelDemanda.Id)
            {
                return BadRequest();
            }

            db.Entry(nivelDemanda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelDemandaExists(id))
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

        // POST: api/NivelesDemanda
        [ResponseType(typeof(NivelDemanda))]
        public IHttpActionResult PostNivelDemanda(NivelDemanda nivelDemanda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NivelDemanda.Add(nivelDemanda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nivelDemanda.Id }, nivelDemanda);
        }

        // DELETE: api/NivelesDemanda/5
        [ResponseType(typeof(NivelDemanda))]
        public IHttpActionResult DeleteNivelDemanda(int id)
        {
            NivelDemanda nivelDemanda = db.NivelDemanda.Find(id);
            if (nivelDemanda == null)
            {
                return NotFound();
            }

            db.NivelDemanda.Remove(nivelDemanda);
            db.SaveChanges();

            return Ok(nivelDemanda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NivelDemandaExists(int id)
        {
            return db.NivelDemanda.Count(e => e.Id == id) > 0;
        }
    }
}
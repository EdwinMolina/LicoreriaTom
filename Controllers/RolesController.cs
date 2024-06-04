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
    public class RolesController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();

        // GET: api/Roles
        public IHttpActionResult GetRol()
        {
            var roles = db.Rol.Select(r => new RolDTO
            {
                Id = r.Id,
                Codigo = r.Codigo,
                TipoRol = r.TipoRol
            }).ToList();

            return Ok(roles);
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Rol))]
        public IHttpActionResult GetRol(int id)
        {
            var rol = db.Rol.Where(r => r.Id == id).Select(r => new RolDTO
            {
                Id = r.Id,
                Codigo = r.Codigo,
                TipoRol = r.TipoRol
            }).FirstOrDefault();

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRol(int id, Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rol.Id)
            {
                return BadRequest();
            }

            db.Entry(rol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/Roles
        [ResponseType(typeof(Rol))]
        public IHttpActionResult PostRol(Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rol.Add(rol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rol.Id }, rol);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(Rol))]
        public IHttpActionResult DeleteRol(int id)
        {
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            db.Rol.Remove(rol);
            db.SaveChanges();

            return Ok(rol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolExists(int id)
        {
            return db.Rol.Count(e => e.Id == id) > 0;
        }
    }
}
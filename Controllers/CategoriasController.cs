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
    public class CategoriasController : ApiController
    {
        private TicTomEntities db = new TicTomEntities();   

        // GET: api/Categorias
        public IHttpActionResult GetCategoria()
        {
            var categorias = db.Categoria.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).ToList();

            return Ok(categorias);
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategoria(int id)
        {
            var categoria = db.Categoria.Where(c => c.Id == id).Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).FirstOrDefault();

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);

        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categoria.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.Id }, categoria);
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categoria.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categoria.Count(e => e.Id == id) > 0;
        }
    }
}
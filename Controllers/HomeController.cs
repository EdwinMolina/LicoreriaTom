using LicoreriaTom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicoreriaTom.Controllers
{
    public class HomeController : Controller
    {
        private TicTomEntities db = new TicTomEntities();
        public ActionResult Index()
        {
            var totalCategorias = db.Categoria.Count();
            ViewBag.TotalCategorias = totalCategorias;

            var totalProductos = db.Producto.Count();
            ViewBag.TotalProductos = totalProductos;

            var totalVentas = db.Venta.Count();
            ViewBag.TotalVentas = totalVentas;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
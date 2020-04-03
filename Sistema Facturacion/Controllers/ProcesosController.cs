using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Facturacion.Models;

namespace Sistema_Facturacion.Controllers
{
    public class ProcesosController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Procesos
        public ActionResult Index()
        {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            return View();
        }

        // GET: Procesos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Procesos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Procesos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Procesos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Procesos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Procesos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Procesos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

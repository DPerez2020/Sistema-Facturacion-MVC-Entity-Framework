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
        public ActionResult Entradas()
        {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            return View();
        }

        // POST: Procesos/Create
        [HttpPost]
        public ActionResult Agregar(Entradas entradas)
        {
            using (var transaccion=db.Database.BeginTransaction()) { 
                try
                {
                    entradas.Fecha = DateTime.Now;

                    db.Entradas.Add(entradas);
                    db.SaveChanges();
                    var existencia = db.Existencias.FirstOrDefault(x => x.ProductoId == entradas.ProductoId);

                    existencia.ProductoId = entradas.ProductoId;
                    existencia.Cantidad += entradas.Cantidad;
                    db.Entry(existencia).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    transaccion.Commit();
                    
                    return RedirectToAction("Entradas");
                }
                catch( Exception ex)
                {
                    transaccion.Rollback();
                    return View();
                }
            }
        }
    }
}

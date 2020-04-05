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

        [HttpPost]
        public ActionResult Agregar(Entradas entradas)
        {
            using (var transaccion=db.Database.BeginTransaction()) { 
                try
                {
                    entradas.Fecha = DateTime.Now;

                    db.Entradas.Add(entradas);
                    var existencia = db.Existencias.FirstOrDefault(x => x.ProductoId == entradas.ProductoId);
                    existencia.Cantidad += entradas.Cantidad;
                    db.Entry(existencia).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    transaccion.Commit();
                    
                    return RedirectToAction("Entradas");
                }
                catch
                {
                    transaccion.Rollback();
                    return View("Entradas");
                }
            }
        }
        public ActionResult Facturacion() {
            ViewBag.ListaClientes = db.Clientes.ToList();
            ViewBag.ListaProducto = db.Productos.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Facturar(Facturacion facturacion) {
            try
            {
                var detallefactura = facturacion.DetalleFacturas;
                facturacion.DetalleFacturas = null;
                facturacion.Fecha = DateTime.Now;
                db.Facturacions.Add(facturacion);
                db.SaveChanges();
                var productos = db.Productos.ToList();
                var idfactura = db.Facturacions.Max(x => x.Id);
                foreach (var item in detallefactura)
                {
                    var idProducto = productos.FirstOrDefault(x=>x.Nombre==item.Producto.Nombre && x.Precio==item.Producto.Precio);
                    DetalleFactura detalleFactura = new DetalleFactura();
                    detalleFactura.FacturacionId = idfactura;
                    detalleFactura.ProductoId = idProducto.Id;
                    detalleFactura.CantidadProducto = item.CantidadProducto;
                    detalleFactura.Total = item.CantidadProducto * item.Producto.Precio;
                    db.DetalleFacturas.Add(detalleFactura);
                }
                db.SaveChanges();
                return View("Facturacion");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Procesos/Create
    }
}

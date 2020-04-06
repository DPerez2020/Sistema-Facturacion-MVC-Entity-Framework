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
        public ActionResult cargarProductos(int proveedorid) {
            var productos = db.Productos.Where(x => x.ProveedorId == proveedorid).ToList();

            return Json(productos);
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

        public ActionResult obtenerClientes(int idcliente) {
            var cliente = db.Clientes.Find(idcliente);
            var categoria = db.Categorias.Find(cliente.CategoriaId).Descripcion;
            return Json(new {cliente,categoria});
        }

        [HttpPost]
        public ActionResult Facturar(Facturacion facturacion) {
            try
            {
                var detallefactura = facturacion.DetalleFacturas;
                var cliente = db.Clientes.Find(facturacion.Id_Cliente);
                var categoria = db.Categorias.Find(cliente.CategoriaId);
                var totalApagar = facturacion.TotalProducto + (facturacion.TotalProducto * (decimal)0.18);
                if (categoria.Descripcion == "Premiun")
                {
                    facturacion.TotalProducto -= facturacion.TotalProducto * (decimal)0.05;
                }
                facturacion.DetalleFacturas = null;
                facturacion.Fecha = DateTime.Now;
                db.Facturacions.Add(facturacion);
                db.SaveChanges();
                var productos = db.Productos.ToList();
                var idfactura = db.Facturacions.Max(x => x.Id);
                var existenciaProducto = db.Existencias.ToList();
                foreach (var item in detallefactura)
                {
                    var idProducto = productos.FirstOrDefault(x=>x.Nombre==item.Producto.Nombre && x.Precio==item.Producto.Precio);
                    DetalleFactura detalleFactura = new DetalleFactura();
                    detalleFactura.FacturacionId = idfactura;
                    detalleFactura.ProductoId = idProducto.Id;
                    detalleFactura.CantidadProducto = item.CantidadProducto;
                    detalleFactura.Total = item.CantidadProducto * item.Producto.Precio;
                    db.DetalleFacturas.Add(detalleFactura);

                    //Actualizacion del stock
                    var exisProduct = existenciaProducto.Find(x => x.ProductoId == detalleFactura.ProductoId);
                    exisProduct.Cantidad -= detalleFactura.CantidadProducto;
                    db.Entry(exisProduct).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Facturacion");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult HistorialEntradas() {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            var entradas = from a in db.Entradas join  b in db.Productos on a.ProductoId equals b.Id
                           join c in db.Proveedores on a.ProveedorId equals c.Id
                           select new VistaEntrada { Fecha=a.Fecha,Producto=b.Nombre,Proveedor=c.Nombre,Cantidad=a.Cantidad };
            return View(entradas.ToList());
        }

        [HttpPost]
        public ActionResult HistorialEntradas(int? productoId,DateTime? fecha,int? proveedorId,string suma,string promedio,string conteo)
        {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            if (productoId == null && fecha == null && proveedorId == null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad,precio=b.Precio };


                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x=>x.precio);
                    ViewBag.Conteo = entradas.Count();
                }

                return View(entradas.ToList());
            }
            else if (productoId == null && fecha == null && proveedorId != null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio=b.Precio};
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else if (productoId == null && fecha != null && proveedorId == null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where a.Fecha>=fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio=b.Precio};
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else if (productoId != null && fecha == null && proveedorId == null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad,precio=b.Precio };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else if (productoId != null && fecha != null && proveedorId == null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad,precio=b.Precio };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else if (productoId != null && fecha == null && proveedorId != null)
            {

                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId && proveedorId == a.ProveedorId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad,precio=b.Precio };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else if (productoId == null && fecha != null && proveedorId != null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio=b.Precio };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
            else {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada && productoId==a.ProductoId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad,precio=b.Precio };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = entradas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = entradas.Average(x => x.precio);
                    ViewBag.sumatoria = entradas.Sum(x => x.precio);
                    ViewBag.Conteo = entradas.Count();
                }
                return View(entradas.ToList());
            }
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
                return RedirectToAction("Facturacion");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        public ActionResult detalleFactura(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion factura = db.Facturacions.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }

            // var detalleFactura = db.DetalleFacturas.Where(x => x.FacturacionId == factura.Id);
            // var productos = db.Productos.Where(x=>x.Id detalleFactura.);
            var detalleFactura = from a in db.DetalleFacturas
                                 join b in db.Productos
                                 on a.ProductoId equals b.Id
                                 select new VistaDetalle { CantidadProducto = a.CantidadProducto,Total=a.Total,Producto=b.Nombre, Precio = b.Precio };
            ViewBag.DetalleFactura = detalleFactura.ToList();
            var facturas = from a in db.Facturacions
                           join b in db.Clientes on a.Id_Cliente equals b.Id
                           where a.Id == id
                           select new VistaFactura
                           {
                               Id = a.Id,
                               Cliente = b.Nombre,
                               TotalProducto = a.TotalProducto,
                               TotalPagado = a.TotalPagado,
                               Fecha = a.Fecha
                           };
            return View(facturas.ToList().First());
        }

        public ActionResult HistorialFacturas() {

            ViewBag.Listacliente = db.Clientes.ToList();
            var facturas = from a in db.Facturacions
                           join b in db.Clientes on a.Id_Cliente equals b.Id
                           select new VistaFactura
                           {
                               Id=a.Id,
                               Cliente=b.Nombre,
                               TotalProducto=a.TotalProducto,
                               TotalPagado=a.TotalPagado,
                               Fecha=a.Fecha,
                               DetalleFacturas=a.DetalleFacturas
                           };

            return View(facturas.ToList());
        }
        [HttpPost]
        public ActionResult HistorialFacturas(DateTime? fecha, int? clienteId, string suma, string promedio, string conteo)
        {
            ViewBag.Listacliente = db.Clientes.ToList();
            if (fecha != null && clienteId == null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var facturas = from a in db.Facturacions
                               join b in db.Clientes on a.Id_Cliente equals b.Id
                               where a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaFactura
                               {
                                   Id = a.Id,
                                   Cliente = b.Nombre,
                                   TotalProducto = a.TotalProducto,
                                   TotalPagado = a.TotalPagado,
                                   Fecha = a.Fecha,
                                   DetalleFacturas = a.DetalleFacturas
                               };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                return View(facturas.ToList());
            }
            else if (clienteId != null && fecha == null)
            {

                var facturas = from a in db.Facturacions
                               join b in db.Clientes on a.Id_Cliente equals b.Id
                               where a.Id_Cliente == clienteId
                               select new VistaFactura
                               {
                                   Id = a.Id,
                                   Cliente = b.Nombre,
                                   TotalProducto = a.TotalProducto,
                                   TotalPagado = a.TotalPagado,
                                   Fecha = a.Fecha,
                                   DetalleFacturas = a.DetalleFacturas
                               };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                return View(facturas.ToList());
            }
            else if (fecha==null && clienteId==null) {
                var facturas = from a in db.Facturacions
                               join b in db.Clientes on a.Id_Cliente equals b.Id
                               select new VistaFactura
                               {
                                   Id = a.Id,
                                   Cliente = b.Nombre,
                                   TotalProducto = a.TotalProducto,
                                   TotalPagado = a.TotalPagado,
                                   Fecha = a.Fecha,
                                   DetalleFacturas = a.DetalleFacturas
                               };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                return View(facturas.ToList());
            }
            else {
                var fechatransformada = fecha.Value.AddDays(1);
                var facturas = from a in db.Facturacions
                               join b in db.Clientes on a.Id_Cliente equals b.Id
                               where a.Fecha >= fecha.Value && a.Fecha <= fechatransformada && a.Id_Cliente == clienteId
                               select new VistaFactura
                               {
                                   Id = a.Id,
                                   Cliente = b.Nombre,
                                   TotalProducto = a.TotalProducto,
                                   TotalPagado = a.TotalPagado,
                                   Fecha = a.Fecha,
                                   DetalleFacturas = a.DetalleFacturas
                               };
                if (suma == null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo == null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = 0;
                }
                else if (suma != null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio == null && conteo != null)
                {
                    ViewBag.promedio = 0;
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }

                else if (suma == null && promedio != null && conteo != null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = facturas.Count();
                }
                else if (suma == null && promedio != null && conteo == null)
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = 0;
                    ViewBag.Conteo = 0;
                }
                else
                {
                    ViewBag.promedio = facturas.Average(x => x.TotalPagado);
                    ViewBag.sumatoria = facturas.Sum(x => x.TotalPagado);
                    ViewBag.Conteo = facturas.Count();
                }
                return View(facturas.ToList());
            }
        }
    }
}

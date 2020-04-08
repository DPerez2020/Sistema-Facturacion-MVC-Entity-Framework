using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rotativa;
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
        public ActionResult printHistorial(int? productoId, DateTime? fecha, int? proveedorId, string suma, string promedio, string conteo)
        {
            return new ActionAsPdf("generarReportEntrada", new {productoId,fecha,proveedorId,suma,promedio,conteo})
            {
                FileName = "Reporte historial.pdf",
            };
        }

        public ActionResult generarReportEntrada(int? productoId, DateTime? fecha, int? proveedorId, string suma, string promedio, string conteo)
        {

            var historial = getdata(productoId, fecha, proveedorId, suma, promedio, conteo);
            return View("~/Views/VistasReportes/ReporteEntradas.cshtml", historial);
        }

        public ActionResult HistorialEntradas() {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            var entradas = from a in db.Entradas join  b in db.Productos on a.ProductoId equals b.Id
                           join c in db.Proveedores on a.ProveedorId equals c.Id
                           select new VistaEntrada { Fecha=a.Fecha,Producto=b.Nombre,Proveedor=c.Nombre,Cantidad=a.Cantidad };
            return View(entradas.ToList());
        }

        private List<VistaEntrada> getdata(int? productoId, DateTime? fecha, int? proveedorId, string suma, string promedio, string conteo) {
            if (productoId == null && fecha == null && proveedorId == null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId == null && fecha == null && proveedorId != null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId == null && fecha != null && proveedorId == null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId != null && fecha == null && proveedorId == null)
            {
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId != null && fecha != null && proveedorId == null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId != null && fecha == null && proveedorId != null)
            {

                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where productoId == a.ProductoId && proveedorId == a.ProveedorId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else if (productoId == null && fecha != null && proveedorId != null)
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }
            else
            {
                var fechatransformada = fecha.Value.AddDays(1);
                var entradas = from a in db.Entradas
                               join b in db.Productos on a.ProductoId equals b.Id
                               join c in db.Proveedores on a.ProveedorId equals c.Id
                               where proveedorId == a.ProveedorId && a.Fecha >= fecha.Value && a.Fecha <= fechatransformada && productoId == a.ProductoId
                               select new VistaEntrada { Fecha = a.Fecha, Producto = b.Nombre, Proveedor = c.Nombre, Cantidad = a.Cantidad, precio = b.Precio };
                calculos(entradas.ToList(), suma, promedio, conteo);
                return entradas.ToList();
            }

        }
        private void calculos(List<VistaEntrada> entradas,string suma,string promedio,string conteo) {
            if (suma != null)
            {
                ViewBag.suma = entradas.Sum(x => x.precio);
            }
            if (promedio != null)
            {
                ViewBag.promedio = entradas.Average(x => x.precio);
            }
            if (conteo != null)
            {
                ViewBag.conteo = entradas.Count;
            }
        }

        [HttpPost]
        public ActionResult HistorialEntradas(int? productoId,DateTime? fecha,int? proveedorId,string suma,string promedio,string conteo)
        {
            ViewBag.ListaProveedores = db.Proveedores.ToList();
            ViewBag.ListaProductos = db.Productos.ToList();
            var datos = getdata(productoId,fecha,proveedorId,suma,promedio,conteo);
            return View(datos);
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

        public ActionResult Recibo()
        {
            var IdFactura = db.Facturacions.Max(x => x.Id);
            var detalleFactura = from a in db.DetalleFacturas
                                 join b in db.Productos
                                 on a.ProductoId equals b.Id
                                 where a.FacturacionId == IdFactura
                                 select new VistaFactura()
                                 {
                                     NombreProducto = b.Nombre,
                                     PrecioProduco = b.Precio,
                                     CantidadProducto = a.CantidadProducto,
                                     Total = a.Total
                                 };

            var Factura = db.Facturacions.Find(IdFactura);
            var cliente = db.Clientes.Find(Factura.Id_Cliente);
            var detalle = db.DetalleFacturas.Where(x => x.FacturacionId == Factura.Id);
            ViewBag.Detalle = detalleFactura;
            ViewBag.cliente = cliente;
            ViewBag.categoria = db.Categorias.Find(cliente.CategoriaId).Descripcion;
            return View("~/Views/VistasReportes/Recibo.cshtml",Factura);
        }
        public ActionResult print()
        {
            return new ActionAsPdf("Recibo")
            {
                FileName = "factura.pdf",
            };
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
                return RedirectToAction("print");
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

            var detalleFactura = from a in db.DetalleFacturas
                                 join b in db.Productos
                                 on a.ProductoId equals b.Id
                                 where a.FacturacionId==factura.Id
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

        private List<VistaFactura> getdataFactura(DateTime? fecha, int? clienteId, string suma, string promedio, string conteo,string mayor,string menor) {
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

                calculosFactura(facturas.ToList(), suma, promedio, conteo, mayor, menor);

                return facturas.ToList();
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
                calculosFactura(facturas.ToList(), suma, promedio, conteo, mayor, menor);
                return facturas.ToList();
            }
            else if (fecha == null && clienteId == null)
            {
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
                calculosFactura(facturas.ToList(), suma, promedio, conteo,mayor, menor);
                return facturas.ToList();
            }
            else
            {
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
                calculosFactura(facturas.ToList(), suma, promedio, conteo,mayor,menor);
                return facturas.ToList();
            }

        }

        private void calculosFactura(List<VistaFactura> facturas,string suma, string promedio, string conteo,string mayor,string menor) {

            if (suma != null)
            {
                ViewBag.suma = facturas.Sum(x => x.TotalPagado);
            }
            if (promedio != null) {
                ViewBag.promedio = facturas.Average(x => x.TotalPagado);
            }
            if (conteo != null) { 
                ViewBag.conteo = facturas.Count;
            }
            if (mayor != null)
            {
                ViewBag.mayor = facturas.Max(x => x.TotalPagado);

            }
            if (menor != null)
            {
                ViewBag.menor = facturas.Min(x => x.TotalPagado);
            }
        }

        [HttpPost]
        public ActionResult printFactura(DateTime? fecha, int? clienteId, string suma_, string promedio_, string conteo_,string mayor,string menor)
        {
            return new ActionAsPdf("generarReportFactura", new { fecha, clienteId, suma_, promedio_, conteo_,mayor,menor })
            {
                FileName = "Reporte facttura.pdf",
            };
        }

        public ActionResult generarReportFactura(DateTime? fecha, int? clienteId, string suma_, string promedio_, string conteo_, string mayor, string menor)
        {

            var factura = getdataFactura(fecha, clienteId, suma_,promedio_,conteo_,mayor,menor);
            return View("~/Views/VistasReportes/ReporteFactura.cshtml", factura);
        }

        [HttpPost]
        public ActionResult HistorialFacturas(DateTime? fecha, int? clienteId, string suma, string promedio, string conteo, string mayor, string menor)
        {
            ViewBag.Listacliente = db.Clientes.ToList();
            var factura = getdataFactura(fecha, clienteId, suma, promedio, conteo,mayor,menor);
            return View(factura);
        }
    }
}

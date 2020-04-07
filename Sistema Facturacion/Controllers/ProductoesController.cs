using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Facturacion.Models;

namespace Sistema_Facturacion.Controllers
{
    public class ProductoesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Productoes
        public ActionResult Index()
        {
            //var data=db.Proveedores.Include(x => x.Productos).ToList();
            var productos = from a in db.Productos
                            join b in db.Proveedores
                            on a.ProveedorId equals b.Id
                            select new VistaProductos { id = a.Id, producto = a.Nombre, Precio = a.Precio, proveedor = b.Nombre };
            return View("Index", productos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string busqueda)
        {
            if (busqueda == string.Empty)
            {
                var productos = from a in db.Productos
                                join b in db.Proveedores
                                on a.ProveedorId equals b.Id
                                select new VistaProductos { id = a.Id, producto = a.Nombre, Precio = a.Precio, proveedor = b.Nombre };
                return View("Index", productos.ToList());
            }
            else
            {
                var productos = from a in db.Productos
                                join b in db.Proveedores
                                on a.ProveedorId equals b.Id
                                where a.Nombre.StartsWith(busqueda)
                                select new VistaProductos { id = a.Id, producto = a.Nombre, Precio = a.Precio, proveedor = b.Nombre };
                return View("Index", productos.ToList());
            }
        }
        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            var proveedor = db.Proveedores.Find(producto.ProveedorId);
            var data = new Proveedor()
            {
                Nombre = proveedor.Nombre,
                Productos = new List<Producto>() {
                    new Producto() {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Precio = producto.Precio,
                        ProveedorId = producto.ProveedorId
                    }
                }
            };

            return View(data);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.lista = db.Proveedores.ToList();
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Precio,ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                var idProducto = db.Productos.Max(x => x.Id);

                Existencia existencia = new Existencia();
                existencia.ProductoId = idProducto;
                existencia.Cantidad = 0;
                db.Existencias.Add(existencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.lista = db.Proveedores.ToList();
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio,ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

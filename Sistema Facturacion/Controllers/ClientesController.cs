using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Facturacion.Models;
using Sistema_Facturacion.infraestructure;
using System.Threading.Tasks;

namespace Sistema_Facturacion.Controllers
{
    public class ClientesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Clientes
        public  ActionResult Index()
        {
            ViewBag.lista = db.Categorias.ToList();
            var clientes = from a in db.Clientes
                            join b in db.Categorias
                            on a.CategoriaId equals b.Id
                            select new VistaClientes { id = a.Id,RNC_Cedula=a.RNC_Cedula, nombre = a.Nombre,telefono = a.Telefono, email = a.Email,categoria=b.Descripcion };
            return View("Index", clientes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string nombre,int? categoria)
        {
            ViewBag.lista = db.Categorias.ToList();
            if (nombre == string.Empty && categoria == null)
            {
                var clientes = from a in db.Clientes
                               join b in db.Categorias
                               on a.CategoriaId equals b.Id
                               select new VistaClientes { id = a.Id, RNC_Cedula = a.RNC_Cedula, nombre = a.Nombre, telefono = a.Telefono, email = a.Email, categoria = b.Descripcion };
                return View("Index", clientes.ToList());
            }
            else if (nombre != string.Empty && categoria != null)
            {
                var clientes = from a in db.Clientes
                               join b in db.Categorias
                               on a.CategoriaId equals b.Id
                               where a.Nombre.StartsWith(nombre) && a.CategoriaId == categoria
                               select new VistaClientes { id = a.Id, RNC_Cedula = a.RNC_Cedula, nombre = a.Nombre, telefono = a.Telefono, email = a.Email, categoria = b.Descripcion };
                ViewBag.Conteo = clientes.ToList().Count;
                return View("Index", clientes.ToList());
            }
            else if (nombre == string.Empty && categoria != null)
            {
                var clientes = from a in db.Clientes
                               join b in db.Categorias
                               on a.CategoriaId equals b.Id
                               where a.CategoriaId == categoria
                               select new VistaClientes { id = a.Id, RNC_Cedula = a.RNC_Cedula, nombre = a.Nombre, telefono = a.Telefono, email = a.Email, categoria = b.Descripcion };
                ViewBag.Conteo = clientes.ToList().Count;
                return View("Index", clientes.ToList());

            }
            else {
                var clientes = from a in db.Clientes
                               join b in db.Categorias
                               on a.CategoriaId equals b.Id
                               where a.Nombre.StartsWith(nombre)
                               select new VistaClientes { id = a.Id, RNC_Cedula = a.RNC_Cedula, nombre = a.Nombre, telefono = a.Telefono, email = a.Email, categoria = b.Descripcion };
                return View("Index", clientes.ToList());
            }
        }


        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            var categoria = db.Categorias.Find(cliente.CategoriaId);

            var data = new Categoria()
            {
                Descripcion = categoria.Descripcion,
                Clientes = new List<Cliente>() {
                    new Cliente(){
                        Id=cliente.Id,
                        Nombre=cliente.Nombre,
                        RNC_Cedula=cliente.RNC_Cedula,
                        Telefono=cliente.Telefono,
                        Email=cliente.Email,
                        CategoriaId=cliente.CategoriaId
                    }
                }
            };
            return View(data);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {   
            ViewBag.lista = db.Categorias.ToList();
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RNC_Cedula,Nombre,Telefono,Email,CategoriaId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.lista = db.Categorias.ToList();
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RNC_Cedula,Nombre,Telefono,Email,CategoriaId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = (Cliente)db.Personas.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = (Cliente)db.Personas.Find(id);
            db.Personas.Remove(cliente);
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

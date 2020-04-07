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
    public class ProveedorsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Proveedors
        public ActionResult Index()
        {
        string query = "SELECT [Id],[RNC_Cedula],[Nombre],[Telefono],[Email],[Discriminator] FROM[SistemaFacturacion].[dbo].[Personas] where Discriminator = 'Proveedor'";
        IEnumerable<Proveedor> data = db.Database.SqlQuery<Proveedor>(query);

            return View(data.ToList());
        }
        [HttpPost]
        public ActionResult Index(string nombre,string email)
        {
            IEnumerable<Proveedor> data;
            if (nombre != string.Empty && email != string.Empty)
            {
                string query = "SELECT [Id],[RNC_Cedula],[Nombre],[Telefono],[Email],[Discriminator] " +
                "FROM [SistemaFacturacion].[dbo].[Personas] where Discriminator = 'Proveedor' and Nombre like '" + nombre + "%' and Email like '" + email + "%'";
                data = db.Database.SqlQuery<Proveedor>(query);
            }
            else if (nombre == string.Empty && email == string.Empty)
            {
                string query = "SELECT [Id],[RNC_Cedula],[Nombre],[Telefono],[Email],[Discriminator] " +
                    "FROM [SistemaFacturacion].[dbo].[Personas] where Discriminator = 'Proveedor' ";
                data = db.Database.SqlQuery<Proveedor>(query);
            }
            else if (nombre == string.Empty && email != string.Empty)
            {
                string query = "SELECT [Id],[RNC_Cedula],[Nombre],[Telefono],[Email],[Discriminator] " +
                "FROM [SistemaFacturacion].[dbo].[Personas] where Discriminator = 'Proveedor' and Email like '" + email + "%'";
                data = db.Database.SqlQuery<Proveedor>(query);
            }
            else {
                string query = "SELECT [Id],[RNC_Cedula],[Nombre],[Telefono],[Email],[Discriminator] " +
                "FROM [SistemaFacturacion].[dbo].[Personas] where Discriminator = 'Proveedor' and Nombre like '" + nombre + "%'";
                 data=db.Database.SqlQuery<Proveedor>(query);
            }

            return View(data.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = (Proveedor)db.Personas.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RNC_Cedula,Nombre,Telefono,Email")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = (Proveedor)db.Personas.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RNC_Cedula,Nombre,Telefono,Email")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor =(Proveedor)db.Personas.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = (Proveedor)db.Personas.Find(id);
            db.Personas.Remove(proveedor);
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

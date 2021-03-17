using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaudePublica.Domain.Database;
using SaudePublica.Domain.Database.Entities;

namespace SaudePublica.Controllers
{
    public class ConsultoriosController : Controller
    {
        private DataContext db;

        public ConsultoriosController()
        {
            db = new DataContext();
        }

        public ConsultoriosController(DataContext db)
        {
            this.db = db;
        }

        // GET: Consultorios
        public ActionResult Index()
        {
            return View(db.Consultorios.ToList());
        }

        // GET: Consultorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consultorio consultorio = db.Consultorios
                .FirstOrDefault(x => x.id == id);

            if (consultorio == null)
            {
                return HttpNotFound();
            }
            return View(consultorio);
        }

        // GET: Consultorios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consultorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,numeroSala,andar,setor,latitude,longitude")] Consultorio consultorio)
        {
            if (ModelState.IsValid)
            {
                db.Consultorios.Add(consultorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consultorio);
        }

        // GET: Consultorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consultorio consultorio = db.Consultorios
                .FirstOrDefault(x => x.id == id);

            if (consultorio == null)
            {
                return HttpNotFound();
            }
            return View(consultorio);
        }

        // POST: Consultorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,numeroSala,andar,setor,latitude,longitude")] Consultorio consultorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultorio);
        }

        // GET: Consultorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consultorio consultorio = db.Consultorios
                .FirstOrDefault(x => x.id == id);

            if (consultorio == null)
            {
                return HttpNotFound();
            }
            return View(consultorio);
        }

        // POST: Consultorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultorio consultorio = db.Consultorios
                .FirstOrDefault(x => x.id == id);

            db.Consultorios.Remove(consultorio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            var consultorios = db.Consultorios
                .Include(x => x.profissionals)
                .OrderBy(x => x.nome)
                .ToList();

            return View(consultorios);
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

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

namespace SaudePublica.Views.Doctor
{
    public class EspecialidadesController : Controller
    {
        private DataContext db;

        public EspecialidadesController()
        {
            db = new DataContext();
        }

        public EspecialidadesController(DataContext db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View(db.Especialidades.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Especialidade especialidade = db.Especialidades
                .FirstOrDefault(x => x.id == id);

            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Especialidades.Add(especialidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Especialidade especialidade = db.Especialidades
                .FirstOrDefault(x => x.id == id);

            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Especialidade especialidade = db.Especialidades
                .FirstOrDefault(x => x.id == id);

            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especialidade especialidade = db.Especialidades
                .FirstOrDefault(x => x.id == id);

            db.Especialidades.Remove(especialidade);
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

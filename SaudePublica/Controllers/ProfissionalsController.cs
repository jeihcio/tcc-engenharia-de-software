using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaudePublica.Domain.Database;
using SaudePublica.Domain.Database.Entities;


namespace SaudePublica.Views
{
    public class ProfissionalsController : Controller
    {
        private DataContext db;

        public ProfissionalsController()
        {
            db = new DataContext();
        }

        public ProfissionalsController(DataContext db)
        {
            this.db = db;
        }

        // GET: Profissionals
        public ActionResult Index()
        {
            var profissionais = db.Profissionais.Include(p => p.especialidade);
            return View(profissionais.ToList());
        }

        // GET: Profissionals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Profissional profissional = db.Profissionais.Include(x => x.consultorios).FirstOrDefault(x => x.id == id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // GET: Profissionals/Create
        public ActionResult Create()
        {
            ViewBag.idEspecialidade = new SelectList(db.Especialidades, "id", "descricao");
            ViewBag.sexo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Masculino", Value = "0"},
                new SelectListItem { Selected = false, Text = "Feminino", Value = "1"},
            }, "Value", "Text", 1);

            ViewBag.Consultorios = db.Consultorios.ToList();
            return View();
        }

        // POST: Profissionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                var novoProfissional = Profissional.Create(profissional);
                RemoverConsultoriosNaoSelecionados(novoProfissional, profissional, EntityState.Unchanged);

                db.Profissionais.Add(novoProfissional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEspecialidade = new SelectList(db.Especialidades, "id", "descricao", profissional.idEspecialidade);
            ViewBag.sexo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Masculino", Value = "0"},
                new SelectListItem { Selected = false, Text = "Feminino", Value = "1"},
            }, "Value", "Text", 1);
            ViewBag.Consultorios = db.Consultorios.ToList();

            return View(profissional);
        }

        // GET: Profissionals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Profissional profissional = db.Profissionais
                .Include(x => x.consultorios)
                .FirstOrDefault(x => x.id == id);

            if (profissional == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEspecialidade = new SelectList(db.Especialidades, "id", "descricao", profissional.idEspecialidade);
            ViewBag.sexo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Masculino", Value = "0"},
                new SelectListItem { Selected = false, Text = "Feminino", Value = "1"},
            }, "Value", "Text", 1);
            ViewBag.Consultorios = db.Consultorios.ToList();

            return View(profissional);
        }

        // POST: Profissionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                var novoProfissional = Profissional.Create(profissional);
                RemoverConsultoriosNaoSelecionados(novoProfissional, profissional, EntityState.Unchanged);

                db.Entry(novoProfissional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEspecialidade = new SelectList(db.Especialidades, "id", "descricao", profissional.idEspecialidade);
            ViewBag.sexo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Masculino", Value = "0"},
                new SelectListItem { Selected = false, Text = "Feminino", Value = "1"},
            }, "Value", "Text", 1);
            ViewBag.Consultorios = db.Consultorios.ToList();

            return View(profissional);
        }

        // GET: Profissionals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Profissional profissional = db.Profissionais.Include(x => x.consultorios).FirstOrDefault(x => x.id == id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profissional profissional = db.Profissionais.Find(id);
            db.Profissionais.Remove(profissional);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region private

        private void RemoverConsultoriosNaoSelecionados(Profissional novoProfissional, Profissional profissional, EntityState estado)
        {
            var novos = new List<Consultorio>();
            foreach (var consultorio in profissional.consultorios)
            {
                if (consultorio.isChecked)
                {
                    novos.Add(consultorio);
                    db.Entry(consultorio).State = estado;
                }
            }

            novoProfissional.consultorios.Clear();
            foreach (var consultorio in novos)
                novoProfissional.consultorios.Add(consultorio);
        }

        #endregion

        #region protected

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}

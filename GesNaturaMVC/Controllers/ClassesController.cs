using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GesNaturaMVC.DAL;
using GesPhloraClassLibrary.Models;

namespace GesNaturaMVC.Controllers
{
    public class ClassesController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Classes
        public async Task<ActionResult> Index()
        {
            var classes = db.Classes.Include(c => c.Reino);
            return View(await classes.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classe classe = await db.Classes.FindAsync(id);
            if (classe == null)
            {
                return HttpNotFound();
            }
            return View(classe);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            ViewBag.ReinoID = new SelectList(db.Reinoes, "ID", "Nome");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,ReinoID")] Classe classe)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(classe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ReinoID = new SelectList(db.Reinoes, "ID", "Nome", classe.ReinoID);
            return View(classe);
        }

        // GET: Classes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classe classe = await db.Classes.FindAsync(id);
            if (classe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReinoID = new SelectList(db.Reinoes, "ID", "Nome", classe.ReinoID);
            return View(classe);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,ReinoID")] Classe classe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ReinoID = new SelectList(db.Reinoes, "ID", "Nome", classe.ReinoID);
            return View(classe);
        }

        // GET: Classes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classe classe = await db.Classes.FindAsync(id);
            if (classe == null)
            {
                return HttpNotFound();
            }
            return View(classe);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Classe classe = await db.Classes.FindAsync(id);
            db.Classes.Remove(classe);
            await db.SaveChangesAsync();
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

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
    public class GenerosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Generos
        public async Task<ActionResult> Index()
        {
            var generoes = db.Generoes.Include(g => g.Familia);
            return View(await generoes.ToListAsync());
        }

        // GET: Generos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generoes.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // GET: Generos/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaID = new SelectList(db.Familias, "ID", "Nome");
            return View();
        }

        // POST: Generos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,FamiliaID")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Generoes.Add(genero);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaID = new SelectList(db.Familias, "ID", "Nome", genero.FamiliaID);
            return View(genero);
        }

        // GET: Generos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generoes.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familias, "ID", "Nome", genero.FamiliaID);
            return View(genero);
        }

        // POST: Generos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,FamiliaID")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familias, "ID", "Nome", genero.FamiliaID);
            return View(genero);
        }

        // GET: Generos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generoes.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Genero genero = await db.Generoes.FindAsync(id);
            db.Generoes.Remove(genero);
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

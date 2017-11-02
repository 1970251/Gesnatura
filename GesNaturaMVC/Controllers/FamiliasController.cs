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
    public class FamiliasController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Familias
        public async Task<ActionResult> Index()
        {
            var familias = db.Familias.Include(f => f.Ordem);
            return View(await familias.ToListAsync());
        }

        // GET: Familias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: Familias/Create
        public ActionResult Create()
        {
            ViewBag.OrdemID = new SelectList(db.Ordems, "ID", "Nome");
            return View();
        }

        // POST: Familias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,OrdemID")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Familias.Add(familia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemID = new SelectList(db.Ordems, "ID", "Nome", familia.OrdemID);
            return View(familia);
        }

        // GET: Familias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemID = new SelectList(db.Ordems, "ID", "Nome", familia.OrdemID);
            return View(familia);
        }

        // POST: Familias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,OrdemID")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemID = new SelectList(db.Ordems, "ID", "Nome", familia.OrdemID);
            return View(familia);
        }

        // GET: Familias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = await db.Familias.FindAsync(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Familia familia = await db.Familias.FindAsync(id);
            db.Familias.Remove(familia);
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

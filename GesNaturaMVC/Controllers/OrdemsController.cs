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
    public class OrdemsController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Ordems
        public async Task<ActionResult> Index()
        {
            var ordems = db.Ordems.Include(o => o.Classe);
            return View(await ordems.ToListAsync());
        }

        // GET: Ordems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // GET: Ordems/Create
        public ActionResult Create()
        {
            ViewBag.ClasseID = new SelectList(db.Classes, "ID", "Nome");
            return View();
        }

        // POST: Ordems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,ClasseID")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                db.Ordems.Add(ordem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClasseID = new SelectList(db.Classes, "ID", "Nome", ordem.ClasseID);
            return View(ordem);
        }

        // GET: Ordems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClasseID = new SelectList(db.Classes, "ID", "Nome", ordem.ClasseID);
            return View(ordem);
        }

        // POST: Ordems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,ClasseID")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClasseID = new SelectList(db.Classes, "ID", "Nome", ordem.ClasseID);
            return View(ordem);
        }

        // GET: Ordems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // POST: Ordems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ordem ordem = await db.Ordems.FindAsync(id);
            db.Ordems.Remove(ordem);
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

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
    public class ReinosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Reinos
        public async Task<ActionResult> Index()
        {
            return View(await db.Reinoes.ToListAsync());
        }

        // GET: Reinos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reino reino = await db.Reinoes.FindAsync(id);
            if (reino == null)
            {
                return HttpNotFound();
            }
            return View(reino);
        }

        // GET: Reinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reinos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome")] Reino reino)
        {
            if (ModelState.IsValid)
            {
                db.Reinoes.Add(reino);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reino);
        }

        // GET: Reinos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reino reino = await db.Reinoes.FindAsync(id);
            if (reino == null)
            {
                return HttpNotFound();
            }
            return View(reino);
        }

        // POST: Reinos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome")] Reino reino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reino).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reino);
        }

        // GET: Reinos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reino reino = await db.Reinoes.FindAsync(id);
            if (reino == null)
            {
                return HttpNotFound();
            }
            return View(reino);
        }

        // POST: Reinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reino reino = await db.Reinoes.FindAsync(id);
            db.Reinoes.Remove(reino);
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

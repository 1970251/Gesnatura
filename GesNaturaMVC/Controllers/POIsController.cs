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
    public class POIsController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: POIs
        public async Task<ActionResult> Index()
        {
            var pOIs = db.POIs.Include(p => p.Percurso);
            return View(await pOIs.ToListAsync());
        }

        // GET: POIs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // GET: POIs/Create
        public ActionResult Create(float lat,float lng, int perc)
        {
            POI poi = new POI();
            poi.GPS_Lat = lat;
            poi.GPS_Long = lng;
            poi.PercursoId = perc;
            ViewBag.PercursoId = new SelectList(db.Percursos, "ID", "Nome");
            return View(poi);
        }
      

        // POST: POIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,Descricao,GPS_Lat,GPS_Long,PercursoId,Aprovado")] POI pOI, int perc)
        {
            if (ModelState.IsValid)
            {
                db.POIs.Add(pOI);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Details","Percursos",new { id=perc });
            }

            ViewBag.PercursoId = new SelectList(db.Percursos, "ID", "Nome", pOI.PercursoId);
            return View(pOI);
        }

        // GET: POIs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            ViewBag.PercursoId = new SelectList(db.Percursos, "ID", "Nome", pOI.PercursoId);
            return View(pOI);
        }

        // POST: POIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,Descricao,GPS_Lat,GPS_Long,PercursoId,Aprovado")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PercursoId = new SelectList(db.Percursos, "ID", "Nome", pOI.PercursoId);
            return View(pOI);
        }

        // GET: POIs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = await db.POIs.FindAsync(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            POI pOI = await db.POIs.FindAsync(id);
            db.POIs.Remove(pOI);
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

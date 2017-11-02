using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GesNaturaMVC.DAL;
using GesPhloraClassLibrary.Models;

namespace GesNaturaMVC.Controllers
{
    public class FotoPoisController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: FotoPois
        public async Task<ActionResult> Index()
        {
            var fotoPois = db.FotoPois.Include(f => f.Poi);
            return View(await fotoPois.ToListAsync());
        }

        // GET: FotoPois/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPois fotoPois = await db.FotoPois.FindAsync(id);
            if (fotoPois == null)
            {
                return HttpNotFound();
            }
            return View(fotoPois);
        }

        // GET: FotoPois/Create
        public ActionResult Create()
        {
            ViewBag.PoiID = new SelectList(db.POIs, "ID", "Nome");
            return View();
        }

        // POST: FotoPois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PoiID,Caminho,GPS_Lat,GPS_Long,Aprovado")] FotoPois fotoPois, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(File.FileName);

                    // store the file inside ~/Foto folder
                   string _path = Path.Combine(Server.MapPath("~/Foto"), _FileName);

                    File.SaveAs(_path);
                    string caminho = "Foto/" + _FileName;
                    fotoPois.Caminho = caminho;
                }
                db.FotoPois.Add(fotoPois);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PoiID = new SelectList(db.POIs, "ID", "Nome", fotoPois.PoiID);
            return View(fotoPois);
        }

        // GET: FotoPois/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPois fotoPois = await db.FotoPois.FindAsync(id);
            if (fotoPois == null)
            {
                return HttpNotFound();
            }
            ViewBag.PoiID = new SelectList(db.POIs, "ID", "Nome", fotoPois.PoiID);
            return View(fotoPois);
        }

        // POST: FotoPois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PoiID,Caminho,GPS_Lat,GPS_Long,Aprovado")] FotoPois fotoPois)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotoPois).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PoiID = new SelectList(db.POIs, "ID", "Nome", fotoPois.PoiID);
            return View(fotoPois);
        }

        // GET: FotoPois/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPois fotoPois = await db.FotoPois.FindAsync(id);
            if (fotoPois == null)
            {
                return HttpNotFound();
            }
            return View(fotoPois);
        }

        // POST: FotoPois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FotoPois fotoPois = await db.FotoPois.FindAsync(id);
            db.FotoPois.Remove(fotoPois);
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

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
using System.IO;

namespace GesNaturaMVC.Controllers
{
    public class FotoAtlasController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: FotoAtlas
        public async Task<ActionResult> Index()
        {
            var fotoAtlas = db.FotoAtlas.Include(f => f.Especie);
            return View(await fotoAtlas.ToListAsync());
        }

        // GET: FotoAtlas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoAtlas fotoAtlas = await db.FotoAtlas.FindAsync(id);
            if (fotoAtlas == null)
            {
                return HttpNotFound();
            }
            return View(fotoAtlas);
        }

        // GET: FotoAtlas/Create
        public ActionResult Create()
        {
            ViewBag.EspecieID = new SelectList(db.Especies, "ID", "Nome");
            return View();
        }

        // POST: FotoAtlas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,EspecieID,Caminho,Aprovado")] FotoAtlas fotoAtlas, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(File.FileName);

                    // store the file inside ~/App_Data/uploads folder
                    //string strID = animalFoto.ID.ToString();
                    string _path = Path.Combine(Server.MapPath("~/Foto"), _FileName);

                    File.SaveAs(_path);
                    string caminho = "Foto/" + _FileName;
                    fotoAtlas.Caminho = caminho;
                }
                db.FotoAtlas.Add(fotoAtlas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EspecieID = new SelectList(db.Especies, "ID", "Nome", fotoAtlas.EspecieID);
            return View(fotoAtlas);
        }

        // GET: FotoAtlas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoAtlas fotoAtlas = await db.FotoAtlas.FindAsync(id);
            if (fotoAtlas == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecieID = new SelectList(db.Especies, "ID", "Nome", fotoAtlas.EspecieID);
            return View(fotoAtlas);
        }

        // POST: FotoAtlas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,EspecieID,Caminho,Aprovado")] FotoAtlas fotoAtlas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotoAtlas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EspecieID = new SelectList(db.Especies, "ID", "Nome", fotoAtlas.EspecieID);
            return View(fotoAtlas);
        }

        // GET: FotoAtlas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoAtlas fotoAtlas = await db.FotoAtlas.FindAsync(id);
            if (fotoAtlas == null)
            {
                return HttpNotFound();
            }
            return View(fotoAtlas);
        }

        // POST: FotoAtlas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FotoAtlas fotoAtlas = await db.FotoAtlas.FindAsync(id);
            db.FotoAtlas.Remove(fotoAtlas);
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

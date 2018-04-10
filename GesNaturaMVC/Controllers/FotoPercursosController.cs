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
using Vereyon.Web;

namespace GesNaturaMVC.Controllers
{
    public class FotoPercursosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: FotoPercursos
        public async Task<ActionResult> Index()
        {
            var fotoPercursos = db.FotoPercursos.Include(f => f.Percurso);
            return View(await fotoPercursos.ToListAsync());
        }

        // GET: FotoPercursos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPercursos fotoPercursos = await db.FotoPercursos.FindAsync(id);
            if (fotoPercursos == null)
            {
                return HttpNotFound();
            }
            return View(fotoPercursos);
        }

        // GET: FotoPercursos/Create
        public ActionResult Create(float lat, float lng, int perc)
        {
            FotoPercursos fp = new FotoPercursos();
            fp.GPS_Lat = lat;
            fp.GPS_Long = lng;
            fp.PercursoID = perc;
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome");
            return View(fp);
        }

        // POST: FotoPercursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PercursoID,Caminho,GPS_Lat,GPS_Long,Aprovado")] FotoPercursos fotoPercursos, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                //if (File.ContentLength > 2100000)
                //{
                //    FlashMessage.Warning("Excedido tamanho da imagem");
                //    return RedirectToAction("Index");
                //}
                if (File != null && File.ContentLength > 0)
                    
                {
                    string _FileName = Path.GetFileName(File.FileName);

                    // store the file inside ~/Foto folder
                    //string strID = animalFoto.ID.ToString();
                    string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);

                    File.SaveAs(_path);
                    string caminho ="Content/Images/"+ _FileName;
                    fotoPercursos.Caminho = caminho;
                }
                db.FotoPercursos.Add(fotoPercursos);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", "Percursos", new { id = fotoPercursos.PercursoID });
                
                
            }

            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", fotoPercursos.PercursoID);
            return View(fotoPercursos);
        }

        // GET: FotoPercursos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPercursos fotoPercursos = await db.FotoPercursos.FindAsync(id);
            if (fotoPercursos == null)
            {
                return HttpNotFound();
            }
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", fotoPercursos.PercursoID);
            return View(fotoPercursos);
        }

        // POST: FotoPercursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PercursoID,Caminho,GPS_Lat,GPS_Long,Aprovado")] FotoPercursos fotoPercursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotoPercursos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", fotoPercursos.PercursoID);
            return View(fotoPercursos);
        }

        // GET: FotoPercursos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotoPercursos fotoPercursos = await db.FotoPercursos.FindAsync(id);
            if (fotoPercursos == null)
            {
                return HttpNotFound();
            }
            return View(fotoPercursos);
        }

        // POST: FotoPercursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FotoPercursos fotoPercursos = await db.FotoPercursos.FindAsync(id);
            db.FotoPercursos.Remove(fotoPercursos);
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

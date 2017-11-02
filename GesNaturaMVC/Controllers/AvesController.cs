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
using GesNaturaMVC.ViewModels;

namespace GesNaturaMVC.Controllers
{
    public class AvesController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Aves
        public async Task<ActionResult> Index()
        {
            return View(await db.Aves.ToListAsync());
        }
        public async Task<ActionResult> Validate()
        {
          return View();
        }
    // GET: Aves/Details/5
    public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ave ave = await db.Aves.FindAsync(id);
            
            if (ave == null)
            {
                return HttpNotFound();
            }
            return View(ave);
        }

        // GET: Aves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NomeComum,Ordem,Familia,Genero,Especie,Descricao,Identificacao,Calendario,Abundancia,FotoCaminho")] Ave ave, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
              if (File != null && File.ContentLength > 0)
              {
          //  if (File.ContentLength > (3 * 1024))
          //    {
          //        string notice = "Tamanho máximo excedido";
          //        return View(notice);
          //    }

          // extract only the fielname
          //var fileName = Path.GetFileName(file.FileName);
          //string extension = Path.GetExtension(file.FileName);
          //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string _FileName = Path.GetFileName(File.FileName);
                
                // store the file inside ~/App_Data/uploads folder
                string _path = Path.Combine(Server.MapPath("~/App_Data/fotosAve"), _FileName);
                //var path = Path.Combine(Server.MapPath("~/App_Data/fotosAve"), fileName);
                File.SaveAs(_path);
                string caminho = _path;
                ave.FotoPath = caminho;
              }
                db.Aves.Add(ave);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            FotoAveViewModel fotoAveViewModel = new FotoAveViewModel(ave,File);
            return View(ave);
        }

        // GET: Aves/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ave ave = await db.Aves.FindAsync(id);
            if (ave == null)
            {
                return HttpNotFound();
            }
            return View(ave);
        }

        // POST: Aves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NomeComum,Ordem,Familia,Genero,Especie,Descricao,Identificacao,Calendario,Abundancia")] Ave ave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ave).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ave);
        }

        // GET: Aves/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ave ave = await db.Aves.FindAsync(id);
            if (ave == null)
            {
                return HttpNotFound();
            }
            return View(ave);
        }

        // POST: Aves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ave ave = await db.Aves.FindAsync(id);
            db.Aves.Remove(ave);
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

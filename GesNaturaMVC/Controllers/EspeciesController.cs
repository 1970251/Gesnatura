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
using GesNaturaMVC.ViewModels;

namespace GesNaturaMVC.Controllers
{
    public class EspeciesController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Especies
        public async Task<ActionResult> Index()
        {
            
            var especies = db.Especies.Include(e => e.Genero);
            

            return View(await especies.ToListAsync());
            
        }

        // GET: Especies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = db.Especies.Where(e => e.ID == id).Include("Fotos").
                Include("Genero.Familia.Ordem.Classe.Reino").FirstOrDefault();
           

            EspecieViewModel especieVM = new EspecieViewModel();
            especieVM.Nome = especie.Nome;
            especieVM.NomeCientifico = especie.NomeCientifico;
            especieVM.Genero = especie.Genero;
            especieVM.Descricao = especie.Descricao;
            especieVM.Calendario = especie.Calendario;
            especieVM.Abundancia = especie.Abundancia;
            
            especieVM.Familia = especie.Genero.Familia.Nome;
            especieVM.Ordem =   especie.Genero.Familia.Ordem.Nome;
            especieVM.Classe =  especie.Genero.Familia.Ordem.Classe.Nome;
            especieVM.Reino =   especie.Genero.Familia.Ordem.Classe.Reino.Nome;

            especieVM.listaFotosVM = new List<FotoAtlasViewModel>();
            string str = "~/";
            foreach(var item in especie.Fotos)
            {
                FotoAtlasViewModel fotoVM = new FotoAtlasViewModel();
                fotoVM.ID = item.ID;
                
                fotoVM.Caminho = str+item.Caminho;
                especieVM.listaFotosVM.Add(fotoVM);
            }
            var nextID = db.Especies.OrderBy(i => i.ID)
                     .SkipWhile(i => i.ID != id)
                     .Skip(1)
                     .Select(i => i.ID);

            ViewBag.NextID = nextID;
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especieVM);
        }

        // GET: Especies/Create
        public ActionResult Create()
        {
            ViewBag.GeneroID = new SelectList(db.Generoes, "ID", "Nome");
            return View();
        }

        // POST: Especies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,NomeCientifico,GeneroID,Descricao,Calendario,Abundancia")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                db.Especies.Add(especie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GeneroID = new SelectList(db.Generoes, "ID", "Nome", especie.GeneroID);
            return View(especie);
        }

        // GET: Especies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = await db.Especies.FindAsync(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroID = new SelectList(db.Generoes, "ID", "Nome", especie.GeneroID);
            return View(especie);
        }

        // POST: Especies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,NomeCientifico,GeneroID,Descricao,Calendario,Abundancia")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroID = new SelectList(db.Generoes, "ID", "Nome", especie.GeneroID);
            return View(especie);
        }

        // GET: Especies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = await db.Especies.FindAsync(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // POST: Especies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Especie especie = await db.Especies.FindAsync(id);
            db.Especies.Remove(especie);
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

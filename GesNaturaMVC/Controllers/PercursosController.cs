using GesNaturaMVC.DAL;
using GesNaturaMVC.ViewModels;
using GesPhloraClassLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GesNaturaMVC.Controllers
{
    public class PercursosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Percursos
        public async Task<ActionResult> Index()
        {

            //var percursos = db.Percursos.Include(p => p.POIs);
            return View(await db.Percursos.ToListAsync());
        }

        // GET: Percursos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percurso percurso = db.Percursos.Where(p => p.ID == id).Include("POIs").Include("FotoPercursos").FirstOrDefault();

            PercursoVM percursoVM = new PercursoVM();
            percursoVM.ListaPOIVM = new List<PoiVM>();
            percursoVM.ListaFotoPercursoVM = new List<FotoPercursoVM>();
            percursoVM.ListaFotoPoiVM = new List<FotoPoiVM>();

            percursoVM.ID = percurso.ID;
            percursoVM.Nome = percurso.Nome;
            percursoVM.Descricao = percurso.Descricao;
            percursoVM.Distancia = percurso.Distancia;
            percursoVM.Duracao = percurso.DuracaoAproximada;
            percursoVM.Dificuldade = percurso.Dificuldade;
            percursoVM.Tipologia = percurso.Tipologia;

            percursoVM.Latitude = percurso.GPS_Lat_Inicio;
            percursoVM.Longitude = percurso.GPS_Long_Inicio;
            percursoVM.Kml = percurso.KmlPath;

            foreach (var poi in percurso.POIs)
            {
                PoiVM poiVM = new PoiVM();

                poiVM.Nome = poi.Nome;
                poiVM.Descricao = poi.Descricao;
                poiVM.Latitude = poi.GPS_Lat;
                poiVM.Longitude = poi.GPS_Long;
                percursoVM.ListaPOIVM.Add(poiVM);

                //FotoPoiVM fotoPoiVM = new FotoPoiVM();
                //fotoPoiVM.Caminho = poi.FotoPoi.Caminho;
                //percursoVM.ListaFotoPoiVM.Add(fotoPoiVM);
            }
            
            foreach (var foto in percurso.FotoPercursos)
            {
                FotoPercursoVM fotoVM = new FotoPercursoVM();
                fotoVM.ID = foto.ID;
                fotoVM.GPS_Lat = foto.GPS_Lat;
                fotoVM.GPS_Lng = foto.GPS_Long;
                fotoVM.Caminho = foto.Caminho;
                percursoVM.ListaFotoPercursoVM.Add(fotoVM);
            }
           
            if (percurso == null)
            { 
                return HttpNotFound();
            }
            return View(percursoVM);
        }
        
        // GET: Percursos/Create
        public ActionResult Create()
        {
            ViewBag.POIs = new SelectList(db.POIs, "ID", "Nome");
            ViewBag.FotoPercursos = new SelectList(db.FotoPercursos, "ID", "Nome");
            return View();
        }

        // POST: Percursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,Descricao,Tipologia,Distancia,DuracaoAproximada,Dificuldade,GPS_Lat_Inicio,GPS_Long_Inicio,KmlPath,POIs")] Percurso percurso)
        {

            if (ModelState.IsValid)
            {
                db.Percursos.Add(percurso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.POIs = new SelectList(db.POIs, "ID", "Nome", percurso.POIs);
            ViewBag.FotoPercursos = new SelectList(db.FotoPercursos, "ID", "Nome", percurso.FotoPercursos);
            return View(percurso);
        }

        // GET: Percursos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percurso percurso = await db.Percursos.FindAsync(id);
            if (percurso == null)
            {
                return HttpNotFound();
            }
            return View(percurso);
        }

        // POST: Percursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,Descricao,Tipologia,Distancia,DuracaoAproximada,Dificuldade,GPS_Lat_Inicio,GPS_Long_Inicio,Kml,POIs")] Percurso percurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(percurso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(percurso);
        }

        // GET: Percursos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percurso percurso = await db.Percursos.FindAsync(id);
            if (percurso == null)
            {
                return HttpNotFound();
            }
            return View(percurso);
        }

        // POST: Percursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Percurso percurso = await db.Percursos.FindAsync(id);
            db.Percursos.Remove(percurso);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public async Task<ActionResult> UploadtoDropboxAsync(string folder, string file, string content)
        //{
        //  using (var dbx = new DropboxClient("YOUR TOKEN"))
        //  {
        //    var full = await dbx.Users.GetCurrentAccountAsync();
        //    Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
        //  }
        //  var dbDrop = new DropboxClient("YOUR TOKEN");



        //  using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        //  {
        //    var updated = await dbDrop.Files.UploadAsync(
        //        folder + "/" + file,
        //        WriteMode.Overwrite.Instance,
        //        body: mem);
        //    Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
        //  }

        //  return RedirectToAction("Index");
        //}

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

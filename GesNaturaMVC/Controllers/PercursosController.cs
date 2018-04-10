using GesNaturaMVC.DAL;
using GesNaturaMVC.ViewModels;
using GesPhloraClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GesNaturaMVC.Controllers
{
    public class PercursosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: Percursos
        public ActionResult Index(PercursoVM model = null)
        {
            Percurso percurso = db.Percursos.FirstOrDefault();
            
            //int i;
            //if (model != null)
            //{
            //    i = model.CurrentPageIndex;
            //}
            model = new PercursoVM
            {
                Cust = db.Percursos.ToList(),
                CustDDL = db.Percursos.ToList(),
                Nome = percurso.Nome,
                Distancia = percurso.Distancia,
                Latitude = percurso.GPS_Lat_Inicio,
                Longitude = percurso.GPS_Long_Inicio
                
            };
            

            //var res = (from s in model.Cust
            //           select s);
            //res = res.ToList();
            //if (model.CurrentPageIndex == 0)
            //{
            //    model.CurrentPageIndex = 0;
            //}
            //model.PageSize = 8;
            //model.PageCount = ((res.Count() + model.PageSize - 1) / model.PageSize);
            //if (model.CurrentPageIndex > model.PageCount)
            //{
            //    model.CurrentPageIndex = model.PageCount;
            //}
            //model.Cust = res.Skip(model.CurrentPageIndex * model.PageSize).Take(model.PageSize);

            //model.ID = percurso.ID;
            //model.Nome = percurso.Nome;
            //model.Descricao = percurso.Descricao;
            //model.Distancia = percurso.Distancia;
            //model.Duracao = percurso.DuracaoAproximada;
            //model.Dificuldade = percurso.Dificuldade;
            //model.Tipologia = percurso.Tipologia;

            //model.Latitude = percurso.GPS_Lat_Inicio;
            //model.Longitude = percurso.GPS_Long_Inicio;
            //model.Kml = percurso.KmlPath;

            return View(model);
            //var percursos = db.Percursos.Include(p => p.POIs);

            //return View();
        }
        [HttpPost]
        public ActionResult Index(PercursoVM model, string btn = null)
        {
            Percurso percurso = db.Percursos.FirstOrDefault();

           

            if (model.SortField == null)
            {
                model.SortField = "TipologiaSelecionada";
                model.SortDirection = "ascending";
            }
            #region SortData

            switch (model.SortField)
            {
                case "TipologiaSelecionada":
                    model.Cust = (model.SortDirection == "ascending" ?
                        db.Percursos.OrderBy(p => p.Tipologia) :
                        db.Percursos.OrderByDescending(p => p.Tipologia));
                    break;
                case "DificuldadeSelecionada":
                    model.Cust = (model.SortDirection == "ascending" ?
                        db.Percursos.OrderBy(p => p.Dificuldade) :
                        db.Percursos.OrderByDescending(p => p.Dificuldade));
                    break;
            }

            #endregion

            var ddl = (from d in model.Cust
                       select d);
            model.CustDDL = ddl;

            #region FilterData

            if (!String.IsNullOrEmpty(model.TipologiaSelecionada))
            {
                model.Cust = model.Cust.Where(s => s.Tipologia.ToString().Trim().Equals(model.TipologiaSelecionada.Trim()));
            }
            if (!String.IsNullOrEmpty(model.DificuldadeSelecionada))
            {
                model.Cust = model.Cust.Where(s => s.Dificuldade.ToString().Trim().Equals(model.DificuldadeSelecionada.Trim()));
            }

            #endregion

            var res = (from s in model.Cust
                       select s);
            res = res.ToList();
            if (model.CurrentPageIndex == 0)
            {
                model.CurrentPageIndex = 0;
            }
            model.PageSize = 2;
            model.PageCount = ((res.Count() + model.PageSize - 1) / model.PageSize);
            if (model.CurrentPageIndex > model.PageCount)
            {
                model.CurrentPageIndex = model.PageCount;
            }
            model.Cust = res.Skip(model.CurrentPageIndex * model.PageSize).Take(model.PageSize);

            model.ID = percurso.ID;
            model.Nome = percurso.Nome;
            model.Descricao = percurso.Descricao;
            model.Distancia = percurso.Distancia;
            model.Duracao = percurso.DuracaoAproximada;
            model.Dificuldade = percurso.Dificuldade;
            model.Tipologia = percurso.Tipologia;

            model.Latitude = percurso.GPS_Lat_Inicio;
            model.Longitude = percurso.GPS_Long_Inicio;
            model.Kml = percurso.KmlPath;

            return View(model);
        }
        // GET: Percursos/Details/5
        //[Authorize(Roles = "Supervisor,Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percurso percurso = db.Percursos.Where(p => p.ID == id).Include("POIs").Include("FotoPercursos").Include("PercursoComentarios").FirstOrDefault();

            PercursoVM percursoVM = new PercursoVM();
            percursoVM.ListaPOIVM = new List<PoiVM>();
            percursoVM.ListaFotoPercursoVM = new List<FotoPercursoVM>();
            percursoVM.ListaFotoPoiVM = new List<FotoPoiVM>();
            percursoVM.ListaComentarios = new List<PercursoComentarioVM>();
            
            

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

            foreach (var comentario in percurso.PercursoComentarios)
            {
                PercursoComentarioVM percComent = new PercursoComentarioVM();

                percComent.ID = comentario.ID;
                percComent.Classificacao = comentario.Classificacao;
                percComent.SomaRating += comentario.Classificacao;
                //ViewBag.SomaRating = percComent.SomaRating;
                percComent.ContRating++;
                //ViewBag.ContRating = percComent.ContRating;
                percComent.Comentario = comentario.Comentario;
                percComent.DataHora = comentario.DataHora;
                percursoVM.ListaComentarios.Add(percComent);
                
                
            }


            var ratings = percursoVM.ListaComentarios;

            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Classificacao);
                ViewBag.SomaRating = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.ContRating = ratingCount;
            }
            else
            {
                ViewBag.SomaRating = 0;
                ViewBag.ContRating = 0;
            }
            if (percurso == null)
            { 
                return HttpNotFound();
            }
            return View(percursoVM);
        }
        [Authorize(Roles = "Supervisor,Admin")]
        public ActionResult CreateMap()
        {
           return View();
        }

        // GET: Percursos/Create
        [Authorize(Roles = "Supervisor,Admin")]
        public ActionResult Create(float lat, float lng)
        {
            Percurso percurso = new Percurso();
            percurso.GPS_Lat_Inicio = lat;
            percurso.GPS_Long_Inicio = lng;
            ViewBag.POIs = new SelectList(db.POIs, "ID", "Nome");
            ViewBag.FotoPercursos = new SelectList(db.FotoPercursos, "ID", "Nome");
            return View(percurso);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,Descricao,Tipologia,Distancia,DuracaoAproximada,Dificuldade,GPS_Lat_Inicio,GPS_Long_Inicio,KmlPath,POIs")] Percurso percurso)
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
        [Authorize(Roles ="Admin,Supervisor")]
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

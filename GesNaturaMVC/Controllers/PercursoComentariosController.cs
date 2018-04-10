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
    public class PercursoComentariosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: PercursoComentarios
        public async Task<ActionResult> Index()
        {
            var percursoComentarios = db.PercursoComentarios.Include(p => p.Percurso);
            return View(await percursoComentarios.ToListAsync());
        }

        // GET: PercursoComentarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PercursoComentario percursoComentario = await db.PercursoComentarios.FindAsync(id);
            if (percursoComentario == null)
            {
                return HttpNotFound();
            }

            //var comentarios = db.PercursoComentarios.Where(p => p.PercursoID.Equals(id)).ToList();
            //ViewBag.Comentarios = comentarios;

            //var ratings = db.PercursoComentarios.Where(p => p.PercursoID.Equals(id)).ToList();
            
            //if (ratings.Count() > 0)
            //{
            //    var ratingSum = ratings.Sum(d => d.Classificacao);
            //    ViewBag.RatingSum = ratingSum;
            //    var ratingCount = ratings.Count();
            //    ViewBag.RatingCount = ratingCount;
            //}
            //else
            //{
            //    ViewBag.RatingSum = 0;
            //    ViewBag.RatingCount = 0;
            //}
            //PercursoComentarioVM pComentVM = new PercursoComentarioVM();
            //pComentVM.ListaComentarios = new List<PercursoComentarioVM>();

            //foreach (var percComent in percurso)
            //{
            //    PercursoComentarioVM percComentVM = new PercursoComentarioVM();
            //    percComentVM.ID = percComent.ID;
            //    percComentVM.Comentario = percComent.Comentario;
            //    percComentVM.Classificacao = percComent.Classificacao;
            //    percComentVM.DataHora = percComent.DataHora;

            //    if (percComent.Classificacao > 0)
            //    {
            //        percComentVM.Rating = percComent.Classificacao;
            //        percComentVM.ContRating += percComent.Classificacao;
            //    }
            //    else
            //    {
            //        percComentVM.Rating = 0;
            //        percComentVM.ContRating = 0;
            //    }
            //    percursoVM.ListaComentarios.Add(percComentVM);
            //}



            return View(percursoComentario);
        }

        // GET: PercursoComentarios/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome");
            return View();
        }
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comentario = form["Comment"].ToString();
            var percursoId = int.Parse(form["PercursoID"]);
            var rating = int.Parse(form["Rating"]);

            PercursoComentario artComment = new PercursoComentario()
            {
                PercursoID = percursoId,
                Comentario = comentario,
                Classificacao = rating,
                DataHora = DateTime.Now
            };

            db.PercursoComentarios.Add(artComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Percursos", new { id = percursoId });
        }
        // POST: PercursoComentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PercursoID,Comentario,Classificacao,DataHora")] PercursoComentario percursoComentario)
        {
            if (ModelState.IsValid)
            {
                db.PercursoComentarios.Add(percursoComentario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", percursoComentario.PercursoID);
            return View(percursoComentario);
        }

        // GET: PercursoComentarios/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PercursoComentario percursoComentario = await db.PercursoComentarios.FindAsync(id);
            if (percursoComentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", percursoComentario.PercursoID);
            return View(percursoComentario);
        }

        // POST: PercursoComentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PercursoID,Comentario,Classificacao,DataHora")] PercursoComentario percursoComentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(percursoComentario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PercursoID = new SelectList(db.Percursos, "ID", "Nome", percursoComentario.PercursoID);
            return View(percursoComentario);
        }

        // GET: PercursoComentarios/Delete/5
        [Authorize(Roles ="Admin,Supervisor")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PercursoComentario percursoComentario = await db.PercursoComentarios.FindAsync(id);
            if (percursoComentario == null)
            {
                return HttpNotFound();
            }
            return View(percursoComentario);
        }

        // POST: PercursoComentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PercursoComentario percursoComentario = await db.PercursoComentarios.FindAsync(id);
            db.PercursoComentarios.Remove(percursoComentario);
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

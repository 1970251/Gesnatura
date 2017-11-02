using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GesNaturaMVC.DAL;
using GesPhloraClassLibrary.Models;
using System.IO;

namespace GesNaturaMVC.Controllers
{
    public class AnimalFotosController : Controller
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        // GET: AnimalFotos
        public async Task<ActionResult> Index()
        {
            var animalFotoes = db.AnimalFotoes.Include(a => a.Animal);
            return View(await animalFotoes.ToListAsync());
        }

        // GET: AnimalFotos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFoto animalFoto = await db.AnimalFotoes.FindAsync(id);
            if (animalFoto == null)
            {
                return HttpNotFound();
            }
            return View(animalFoto);
        }

        // GET: AnimalFotos/Create
        public ActionResult Create()
        {
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "NomeComum");
            return View();
        }

        // POST: AnimalFotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,AnimalID,Caminho")] AnimalFoto animalFoto, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(File.FileName);

                    // store the file inside ~/App_Data/uploads folder
                    //string strID = animalFoto.ID.ToString();
                    string _path = Path.Combine(Server.MapPath("~/App_Data/fotosAve"), _FileName);

                    File.SaveAs(_path);
                    string caminho = _path;
                    animalFoto.Caminho = caminho;
                }
                db.AnimalFotoes.Add(animalFoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "NomeComum", animalFoto.AnimalID);
            return View(animalFoto);
        }

        // GET: AnimalFotos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFoto animalFoto = await db.AnimalFotoes.FindAsync(id);
            if (animalFoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "NomeComum", animalFoto.AnimalID);
            return View(animalFoto);
        }

        // POST: AnimalFotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AnimalID,Caminho")] AnimalFoto animalFoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalFoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "NomeComum", animalFoto.AnimalID);
            return View(animalFoto);
        }

        // GET: AnimalFotos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalFoto animalFoto = await db.AnimalFotoes.FindAsync(id);
            if (animalFoto == null)
            {
                return HttpNotFound();
            }
            return View(animalFoto);
        }

        // POST: AnimalFotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AnimalFoto animalFoto = await db.AnimalFotoes.FindAsync(id);
            db.AnimalFotoes.Remove(animalFoto);
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

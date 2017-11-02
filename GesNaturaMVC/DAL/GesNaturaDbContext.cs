using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GesPhloraClassLibrary.Models;
using GesNaturaMVC.Models;

namespace GesNaturaMVC.DAL
{
    public class GesNaturaDbContext : ApplicationDbContext

    {
        public GesNaturaDbContext() : base() { }
        public DbSet<Percurso> Percursos { get; set; }
        public DbSet<POI> POIs { get; set; }
        public DbSet<Reino> Reinoes { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Ordem> Ordems { get; set; }

        public DbSet<Familia> Familias { get; set; }

        public DbSet<Genero> Generoes { get; set; }

        public DbSet<Especie> Especies { get; set; }

        public DbSet<FotoAtlas> FotoAtlas { get; set; }

        public DbSet<FotoPercursos> FotoPercursos { get; set; }

        public DbSet<FotoPois> FotoPois { get; set; }
    }
}
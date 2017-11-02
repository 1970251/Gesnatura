namespace GesNaturaMVC.Migrations
{
    using GesNaturaMVC.DAL;
    using GesPhloraClassLibrary.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GesNaturaMVC.DAL.GesNaturaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GesNaturaMVC.DAL.GesNaturaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //context.Percursos.AddOrUpdate(x => x.ID,
            //    new Percurso()
            //    {
            //        ID = 1,
            //        Nome = "Caminho do Paço",
            //        Descricao = "Percurso junto ao Rio Germinade, onde podemos observar várias " +
            //        "espécies de aves. Inserido em pleno parque N. Sra de Lurdes",
            //        Dificuldade = "Baixa",
            //        Distancia = 2,
            //        DuracaoAproximada = 2,
            //        Tipologia = "Circular",
            //        GPS_Lat_Inicio = 41.002450F,
            //        GPS_Long_Inicio = -8.162835F,
            //        KmlPath = "https://docs.google.com/uc?export=download&id=0Bxc0V7hINajBcmdCOW9YMFNTLWM"

            //    },
            //    new Percurso()
            //    {
            //        ID = 2,
            //        Nome = "Caminho do Loureiro",
            //        Descricao = "Rumo ao monte. Subir ao cimo do monte, e respirar ar puro",
            //        Dificuldade = "Media",
            //        Distancia = 2,
            //        DuracaoAproximada = 2,
            //        Tipologia = "Circular",
            //        GPS_Lat_Inicio = 40.997354F,
            //        GPS_Long_Inicio = -8.161707F,
            //        KmlPath = "https://docs.google.com/uc?export=download&id=0Bxc0V7hINajBdWc5NGRUUmNzRHc"

            //    },
            //    new Percurso()
            //    {
            //        ID = 3,
            //        Nome = "Caminho da Balsa",
            //        Descricao = "Percurso entre 2 maravilhas da Natureza: Parque N. Sra Lurdes" +
            //        " e a ponte da Balsa",
            //        Dificuldade = "Baixa",
            //        Distancia = 2,
            //        DuracaoAproximada = 1,
            //        Tipologia = "Linear",
            //        GPS_Lat_Inicio = 40.998240F,
            //        GPS_Long_Inicio = -8.171382F,
            //        KmlPath = "https://docs.google.com/uc?export=download&id=0Bxc0V7hINajBdEcwcWpzNXZHb2c"

            //    });

            //context.POIs.AddOrUpdate(x => x.ID,
            //     new POI() {
            //         ID =1,
            //         Nome ="Miradouro de Aves",
            //         Descricao ="Local onde é possivel admirar aves de Rapina",
            //         GPS_Lat=40.995009F,
            //         GPS_Long=-8.159134F,
            //         PercursoId=2
            //     },
            //     new POI()
            //     {
            //         ID = 2,
            //         Nome = "Antiga Azenha",
            //         Descricao = "Local de grandes recordações com uma envolvência natural soberba",
            //         GPS_Lat = 41.000737F,
            //         GPS_Long = -8.163427F,
            //         PercursoId = 3
            //     });

            //context.Reinoes.AddOrUpdate(x=>x.ID,
            //    new Reino() { ID = 1, Nome="Animal"},
            //    new Reino() { ID = 2, Nome="Vegetal"});

            //context.Classes.AddOrUpdate(x => x.ID,
            //    new Classe() { ID = 1, Nome = "Aves", ReinoID=1 },
            //    new Classe() { ID = 2, Nome = "Insetos", ReinoID=1 },
            //    new Classe() { ID = 3, Nome = "Arvores", ReinoID = 2 },
            //    new Classe() { ID = 4, Nome = "Repteis", ReinoID = 1 });

            //context.Ordems.AddOrUpdate(x => x.ID,
            //    new Ordem() { ID = 1, Nome = "Passeriformes", ClasseID = 1 },
            //    new Ordem() { ID = 2, Nome = "Fagales", ClasseID = 2 },
            //    new Ordem() { ID = 3, Nome = "Accipitriformes", ClasseID = 1 },
            //    new Ordem() { ID = 4, Nome = "Bucerotiformes", ClasseID = 1 });

            //context.Familias.AddOrUpdate(x => x.ID,
            //    new Familia() { ID = 1, Nome = "Motacillidae", OrdemID = 1 },
            //    new Familia() { ID = 2, Nome = "Paridae", OrdemID = 1 },
            //    new Familia() { ID = 3, Nome = "Passeridae", OrdemID = 1 });
                
            //context.Generoes.AddOrUpdate(x => x.ID,
            //    new Genero() { ID = 1, Nome = "Motacilla", FamiliaID = 1 },
            //    new Genero() { ID = 2, Nome = "Parus",     FamiliaID = 2 },
            //    new Genero() { ID = 3, Nome = "Passer",    FamiliaID = 3 });

            //context.Especies.AddOrUpdate(x => x.ID,
            //    new Especie() { ID = 1, Nome = "Alvéola branca", NomeCientifico = "Motacilla alba", GeneroID = 1 },
            //    new Especie() { ID = 2, Nome = "Chapim-real", NomeCientifico = "Parus major", GeneroID = 2 },
            //    new Especie() { ID = 3, Nome = "Pardal-comum", NomeCientifico = "Passer domesticus", GeneroID = 3 });

            
        }
    }
   
}

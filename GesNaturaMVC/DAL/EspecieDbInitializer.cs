using GesPhloraClassLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.DAL
{
    public class EspecieDbInitializer : CreateDatabaseIfNotExists<GesNaturaDbContext>
    {
        private GesNaturaDbContext db = new GesNaturaDbContext();

        protected override void Seed(GesNaturaDbContext context)
        {
            base.Seed(context);
            createEspecie(context);
            
        }
        private void createEspecie(GesNaturaDbContext context)
        {
            var familia = new Familia { Nome = "Turdidade", OrdemID = 1 };
            var genero = new Genero { Nome = "Turdus", FamiliaID = 5 };
            var especie = new Especie {
                Nome = "Tordo-comum",
                NomeCientifico = "Turdus philomelos",
                GeneroID =5,
                Descricao = "O tordo-comum, tordo-músico ou tordo-pinto (Turdus philomelos)" +
                " é uma ave pertencente ao género Turdus.Ocorre naturalmente na Europa," +
                " Norte de África, Médio Oriente e Sibéria, e foi introduzida na Austrália" +
                " e Nova Zelândia durante a segunda metade do século XIX.Dependendo da" +
                " latitude, pode ser residente, migratória ou parcialmente migratória," +
                " possuindo três subespécies geralmente aceites"
            };
            var especies = db.Especies.FirstOrDefault();

            if ( !especies.NomeCientifico.Contains("Turdus philomelos"))
            {
                //var result = userManager.Create(user, "Admin2015++");
                
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, "Administrador");
                //}
            }

           

        }
    }
}
using GesPhloraClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.ViewModels
{
    public class PercursoComentarioVM
    {
        public int ID { get; set; }
        public int PercursoID { get; set; }
        public Percurso Percurso { get; set; }
        public string Comentario { get; set; }
        public int Classificacao { get; set; }
        public int SomaRating { get; set; }
        //public int Rating { get; set; }
        public int ContRating { get; set; }
        public DateTime DataHora { get; set; }
        public decimal TotalRating { get; set; }
        //public List<PercursoComentarioVM> ListaComentarios { get; set; }
    }
}
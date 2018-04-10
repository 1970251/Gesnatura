using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.ViewModels
{
    public class PercursoVM : GesPhloraClassLibrary.Models.PercursoOrdem
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipologia { get; set; }
        public float Distancia { get; set; }
        public float Duracao { get; set; }
        public string Dificuldade { get; set; }
        public string Kml { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime DataHora { get; set; }
        public List<PoiVM> ListaPOIVM { get; set; }
        public List<FotoPercursoVM> ListaFotoPercursoVM { get; set; }
        public List<FotoPoiVM> ListaFotoPoiVM { get; set; }
        public List<PercursoComentarioVM> ListaComentarios { get; set; }

    }
}
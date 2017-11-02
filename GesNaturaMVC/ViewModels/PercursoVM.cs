using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.ViewModels
{
    public class PercursoVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipologia { get; set; }
        public int Distancia { get; set; }
        public string Kml { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<PoiVM> ListaPOIVM { get; set; }
        public List<FotoPercursoVM> ListaFotoPercursoVM { get; set; }
        public List<FotoPoiVM> ListaFotoPoiVM { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GesPhloraClassLibrary.Models;

namespace GesNaturaMVC.ViewModels
{
    public class EspecieViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string NomeCientifico { get; set; }
        public string Descricao { get; set; }
        public string Calendario { get; set; }
        public string Abundancia { get; set; }
        public Genero Genero { get; set; }
        public string Familia { get; set; }
        public string Ordem { get; set; }
        public string Classe { get; set; }
        public string Reino { get; set; }
        public List<FotoAtlasViewModel> listaFotosVM { get; set; }

    }
}
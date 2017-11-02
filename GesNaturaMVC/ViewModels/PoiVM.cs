using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.ViewModels
{
  public class PoiVM
  {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
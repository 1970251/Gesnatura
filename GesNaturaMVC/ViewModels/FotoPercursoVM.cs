using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GesNaturaMVC.ViewModels
{
    public class FotoPercursoVM
    {
        public int ID { get; set; }
        public string Caminho { get; set; }
        public float GPS_Lat { get; set; }
        public float GPS_Lng { get; set; }
    }
}
using System.Web;
using System.IO;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Server;
using System.Web.Mvc;

namespace GesNaturaMVC.Helpers
{
    public class Utils
    {
        //public static string uploadfile(HttpPostedFileBase File)
        //{
        //    if (File != null && File.ContentLength > 0)
        //    {
        //        string _FileName = Path.GetFileName(File.FileName);

        //        // store the file inside ~/App_Data/uploads folder
        //        //string strID = animalFoto.ID.ToString();
        //        string _path = Path.Combine(Server.MapPath("~/Foto"), _FileName);

        //        File.SaveAs(_path);
        //        string caminho = "Foto/" + _FileName;
        //        return caminho;
        //    }
        //    return ("false");
        //}

        

    }
}
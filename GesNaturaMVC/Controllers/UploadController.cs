using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace GesNaturaMVC.Controllers
{
    public class UploadController : Controller
    {
    // GET: Upload
    public ActionResult Index()
    {
      return View();
    }
    [HttpPost]
    public ActionResult UploadFile(HttpPostedFileBase file)
    {
      try
      {
        if (file.ContentLength > 0)
        {
          string _FileName = Path.GetFileName(file.FileName);
          string _path = Path.Combine(Server.MapPath("~/App_Data"), _FileName);
          file.SaveAs(_path);

          XmlDocument doc = new XmlDocument();
          doc.Load(_path);
          string jsonText = JsonConvert.SerializeXmlNode(doc);
          var newFilePath = Path.Combine(Server.MapPath("~/App_Data/"), _FileName + ".json");
          //Save JsonFile
          using (StreamWriter jsonFile = System.IO.File.CreateText(newFilePath))
          {
            JsonSerializer serializer = new JsonSerializer();
            //serialize object directly into file stream
            serializer.Serialize(jsonFile, jsonText);
          }
        }
        ViewBag.Message = "Upload com sucesso";
        return View();
      }
      catch
      {
        ViewBag.Message = "Erro ao efetuar upload";
        return View();
      }
    }
    [HttpPost]
    public ActionResult UploadFoto(HttpPostedFileBase file)
    {
      //var path = "";
      if(file!=null)
      {
        if (file.ContentLength > 0)
        {
          //if(Path.GetExtension(file.FileName).ToLower()==".jpg"
          //  || Path.GetExtension(file.FileName).ToLower() == ".png"
          //  || Path.GetExtension(file.FileName).ToLower() == ".gif"
          //  || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
          //{
          //  path = Path.Combine(Server.MapPath("~/App_Data/"), file.FileName);
          //  file.SaveAs(path);
          //  ViewBag.UploadSucess = true;
          //}
          string _FileName = Path.GetFileName(file.FileName);
          string _path = Path.Combine(Server.MapPath("~/Foto"), _FileName);
          file.SaveAs(_path);
          ViewBag.UploadSucess = true;

        }
      }
     return View(file);
     // return RedirectToAction("Index");
    }
  }
}
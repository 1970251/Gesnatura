using GesNaturaMVC.DAL;
using GesNaturaMVC.Models;
using GesPhloraClassLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GesNaturaMVC.Startup))]
namespace GesNaturaMVC
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
            ConfigureAuth(app);
            
      
    }
         
       

    }
}

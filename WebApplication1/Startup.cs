using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Note doc url - https://blogs.msdn.microsoft.com/mvpawardprogram/2017/05/02/adding-webapi-oauth-auth/
            //var config = new HttpConfiguration();

            ConfigureAuth(app);

            //app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(config);
        }
    }
}

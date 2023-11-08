using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SystemManagement.Client.Utilities;

namespace SystemManagement.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Information("Application started", DateTime.Now.Ticks);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ConfigViewEngins();
            ConfigDevexpress();
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            Log.Fatal(error.ToString(), DateTime.Now.Ticks);
        }

        private void ConfigDevexpress()
        {
            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
            DevExpress.Web.ASPxWebControl.CallbackError += HandleDevexpressErrors;
        }

        private void ConfigViewEngins()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void HandleDevexpressErrors(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxWebControl.SetCallbackErrorMessage("امکان اجرای درخواست فراهم نشد، لطفا با تیم توسعه تماس حاصل فرمایید");
        }
    }
}
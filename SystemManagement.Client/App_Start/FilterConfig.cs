using System.Web.Mvc;
using SystemManagement.Client.Filters;

namespace SystemManagement.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandlerFilter());
            filters.Add(new JsonResultFilter());
        }
    }
}
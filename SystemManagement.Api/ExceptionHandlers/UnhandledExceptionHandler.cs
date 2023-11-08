using System;
using System.Web.Http.ExceptionHandling;
using SystemManagement.Api.HttpActionResults;

namespace SystemManagement.Api.ExceptionHandlers
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exceptionMessage = context.Exception.GetBaseException().Message.ToString();
#if DEBUG
            var debugContent = Newtonsoft.Json.JsonConvert.SerializeObject(context.Exception);
#else
            var debugContent = @"{ ""Message"" : ""فرآیند اجرا با مشکل مواجه شده است، لطفا با تیم توسعه تماس حاصل فرمایید"" }";
#endif
            Utilities.Log.Fatal(context.Exception.ToString(), DateTime.Now.Ticks);

            context.Result = new ErrorContentResult(debugContent, "application/json", context.Request);
        }
    }
}
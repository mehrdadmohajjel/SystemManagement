﻿using System.Web.Mvc;
using SystemManagement.Shared.Utilities;

namespace SystemManagement.Client.Filters
{
    public class AccessToFormValidationAttribute : ActionFilterAttribute
    {
        public string FormCode { get; set; }

        public string SystemCode { get; set; } = Settings.SystemCode;

        private void CheckAccessToForm(ActionExecutingContext filterContext)
        {
            try
            {
                var tokenInHeader = filterContext.HttpContext.Request.Headers.Get("Authorization");
                var tokenInCookie = filterContext.HttpContext.Request.Cookies.Get("MohajjelJsonWebToken")?.Value;
                if (string.IsNullOrEmpty(tokenInHeader) && string.IsNullOrEmpty(tokenInCookie))
                {
                    filterContext.Result = new RedirectResult("http://localhost");
                }
                else
                {
                    var token = !string.IsNullOrEmpty(tokenInCookie) ? tokenInCookie : tokenInHeader;

                    var userId = UserTools.GetUserIdByJsonWebToken(token);
                    var result = UserTools.HasAccessToForm(userId, FormCode, SystemCode);
                    if (!result)
                    {
                        filterContext.Result = new RedirectResult("~/Forbidden");
                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Forbidden");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckAccessToForm(filterContext);
            base.OnActionExecuting(filterContext);
        }
    }
}
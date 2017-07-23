using SettingConstant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace KidsapWebApi.Filters
{
    public class PrivilegeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string methodType = actionContext.Request.Method.Method;
            var securityToken = actionContext.Request.Headers.GetValues("token").FirstOrDefault();
            if (methodType.ToUpper().Equals("DELETE"))
            {
                if (securityToken != Settings.DELETE_TOKEN)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "access denied");
            }
            else
            {
                if (securityToken != Settings.ACCCESS_TOKEN)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "access denied");
            }
           
            base.OnActionExecuting(actionContext);
        }
    }
}
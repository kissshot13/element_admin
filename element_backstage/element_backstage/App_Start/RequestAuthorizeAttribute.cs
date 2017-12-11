using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Http.Controllers;
using System.Web.Http;

namespace element_backstage.App_Start
{
    public class RequestAuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息,验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization != null && authorization.Parameter != null)
            {
                var encryptTicket = authorization.Parameter;
                if ()
                {

                }
            }
            base.OnAuthorization(actionContext);
        }

        private bool validateTicket(string encryptTicket)
        {
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

            var index = strTicket.IndexOf('&');
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

            
        }
    }
}
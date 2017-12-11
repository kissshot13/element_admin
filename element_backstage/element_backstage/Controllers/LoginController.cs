using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySql.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Message.Request;
using Message.Repose;
using System.Web.Security;
using System.Web;

namespace element_backstage.Controllers
{
    public class LoginController: ApiController
    {        
        private ELementDB  db = new ELementDB();

        //post
        public IHttpActionResult  PostLogin(LoginModel login)
        {

            if (!UserExits(login))
            {
                return Ok(new ResposeBase("用户不存在"));
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, login.UserName, DateTime.Now, DateTime.Now.AddHours(1), true,string.Format("{0}&{1}",login.UserName,login.Password),FormsAuthentication.FormsCookiePath);

            LoginResponseModel info = new LoginResponseModel
            {
               user = new User
               {
                   UserName = login.UserName,
                   Password = "",
               },
               Success = true,
               Ticket = FormsAuthentication.Encrypt(ticket)

            };
            HttpContext.Current.Session["private"] = info;
            
            return Ok(info);
        }

        private bool UserExits(LoginModel login)
        {
            return db.Users.Count(o => o.UserName == login.UserName && o.Password == login.Password && o.Del == false) > 0;
        }
    }
}

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

namespace element_backstage.Controllers
{
    public class AccountController: ApiController
    {

        private ELementDB  db = new ELementDB();

        //post
        public IHttpActionResult  PostLogin(LoginModel login)
        {
            if (!UserExits(login))
            {
                return NotFound();
            }

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, login.UserName, DateTime.Now, DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", login.UserName, login.Password),FormsAuthentication.FormsCookiePath);
            //return new ResposeBase();
            return Ok(login);
            //return new LoginResponseModel(login);
        }

        private bool UserExits(LoginModel login)
        {
            return db.Users.Count(o => o.UserName == login.UserName && o.Del == false) > 0;
        }
    }
}
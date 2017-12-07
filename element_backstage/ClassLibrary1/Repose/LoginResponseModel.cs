using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Message.Repose
{
    public class LoginResponseModel : ResposeBase
    {
        //user 模型
        public User user { get; set; }

        /// <summary>
        /// 返回user对象
        /// </summary>
        /// <param name="user"></param>
        public LoginResponseModel(User user)
        {
            Success = true;
            this.user = user;
        }
    }
}

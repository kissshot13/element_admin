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

        public string Ticket { get; set; }

        public LoginResponseModel() { }

        public LoginResponseModel(User user) {
            this.user = user;
            this.Success = true;
        }

    }
}

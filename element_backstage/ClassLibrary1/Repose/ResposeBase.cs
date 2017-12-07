using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Repose
{
    public class ResposeBase
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 发送错误消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 跳转的新路径
        /// </summary>
        public string NextUrl { get; set; }

        /// <summary>
        /// 默认构造函数，默认回传成功
        /// </summary>
        public ResposeBase()
        {
            Success = true;
        }
        /// <summary>
        /// 传入出错信息
        /// </summary>
        /// <param name="Msg"></param>
        public ResposeBase(string Msg)
        {
            Success = false;
            this.Msg = Msg;
        }
    }
}

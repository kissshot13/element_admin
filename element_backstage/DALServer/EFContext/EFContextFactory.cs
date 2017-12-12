using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace DALServer
{
    public class EFContextFactory
    {
        public static DbContext GetCurrentObjectContext()
        {
            DbContext obcontext = CallContext.GetData("DbContext") as DbContext;

            if (obcontext == null)
            {
                obcontext = new ELementDB();
            }

            return obcontext;
        }
    }
}

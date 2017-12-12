using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDALServer
{
    public interface IUserDAL : IBaseDALServer<User>
    {

    }

    public interface ICustomerDAL: IBaseDALServer<Customer>
    {

    }
}

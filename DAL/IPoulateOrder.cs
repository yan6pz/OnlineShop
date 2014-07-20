using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPoulateOrder
    {
        Order ShowAll(IDataReader arg);
        User CurrentUser(IDataReader arg);
    }
}

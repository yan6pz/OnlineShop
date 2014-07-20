using System.Collections.Generic;
using DAL;

namespace BusinessLayer
{
    public interface IOrderable
    {
        IEnumerable<Order> ShowOrders(string email);
        bool Delete(int id);
        User ShowCurrUser(string currentUser);
    }
}

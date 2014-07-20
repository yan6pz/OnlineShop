using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PopulateOrder : IPoulateOrder
    {
        public Order ShowAll(IDataReader arg)
        {
            var order = new Order();
            order.OrderID = int.Parse(arg["OrderID"].ToString());
            order.Price = decimal.Parse(arg["Price"].ToString());
            order.OrderDate = DateTime.Parse(arg["OrderDate"].ToString());
            order.Quantity = decimal.Parse(arg["Quantity"].ToString());
            return order;
        }

        public User CurrentUser(IDataReader arg)
        {
            var user = new User();
            user.UserID = int.Parse(arg["UserID"].ToString());

            user.UserName = arg["UserName"].ToString();
            user.FirstName = arg["FirstName"].ToString();
            user.LastName = arg["LastName"].ToString();
            user.Email = arg["Email"].ToString();
            user.IsAdmin = bool.Parse(arg["IsAdmin"].ToString());
            return user;
        }
    }
}

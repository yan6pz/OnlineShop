using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PopulateOrderDetails:IPopulateOrderDetails
    {
        public OrderDetail ShowAll(IDataReader arg)
        {
            var orderDet = new OrderDetail();
            orderDet.OrderID = int.Parse(arg["OrderID"].ToString());
            orderDet.ProductName = arg["Name"].ToString();
            orderDet.Price = decimal.Parse(arg["Price"].ToString());
            orderDet.AvailableQTY = decimal.Parse(arg["AvailableQTY"].ToString());
            return orderDet;
        }
    }
}

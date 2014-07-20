using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BusinessLayer
{
    public class OrderDetailActions:IOrderDetail
    {
         public IDatabaseAccess          DatabaseAccessInstance           { get; private set; }
         public PopulateOrderDetails    OrderDetailsModel { get; private set; }

        public OrderDetailActions(DatabaseAccess accessdata
                                                                           , PopulateOrderDetails userModel)
       {
           this.DatabaseAccessInstance                              = accessdata;
           this.OrderDetailsModel = userModel;
       }

        public IEnumerable<OrderDetail> Details(string email)
        {
            var databaseAccessInstance = new DatabaseAccess();
            var userModel = new PopulateOrder();
            IOrderable order = new OrderActions(databaseAccessInstance, userModel);

            var currentUser = order.ShowCurrUser(email);
            
            
            var result = DatabaseAccessInstance.ExecuteReader(SqlCommands.SHOW_USER_ORDERDETAILS
                                                                ,currentUser.UserID.ToString()
                                                                            , OrderDetailsModel.ShowAll);
            var firstOrDefault = result.FirstOrDefault();
            if (firstOrDefault != null)
            {
                firstOrDefault.CurrentUser=new User();
                firstOrDefault.CurrentUser.IsAdmin = currentUser.IsAdmin;
                firstOrDefault.CurrentUser.UserID = currentUser.UserID;
            }
            return result;
        }
    }
}

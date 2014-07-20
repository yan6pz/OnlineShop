using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BusinessLayer
{
    public class OrderActions:IOrderable
    {

         public IDatabaseAccess          DatabaseAccessInstance           { get; private set; }
         public PopulateOrder    OrderModel { get; private set; }

         public OrderActions(DatabaseAccess accessdata
                                                                           , PopulateOrder userModel)
       {
           this.DatabaseAccessInstance                              = accessdata;
           this.OrderModel = userModel;
       }

        public IEnumerable<Order> ShowOrders(string email)
        {
            var result = DatabaseAccessInstance.ExecuteReader(SqlCommands.SHOW_USER_ORDERS
                                                                , email
                                                                            , OrderModel.ShowAll);
            return result;
        }

        public bool Delete(int id)
        {
            var result = DatabaseAccessInstance.DeleteOrders(SqlCommands.DELETE_ORDERS
                                                                , id);
            return result;
        }

        public User ShowCurrUser(string currentUser)
        {
            var result = DatabaseAccessInstance.ExecuteReader(SqlCommands.CURRENT_USER
                                                                , currentUser
                                                                            , OrderModel.CurrentUser);
            return result.FirstOrDefault();
        }
    }
}

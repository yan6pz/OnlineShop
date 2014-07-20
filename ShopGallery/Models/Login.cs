using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class Login
    {
        public static string connString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";
          public static string currentUser = null;

         [Required(ErrorMessage = "Email Id Required")]
         [DisplayName("Email ID")]
         [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 
                                    ErrorMessage = "Email Format is wrong")]
         [StringLength(50, ErrorMessage = "Less than 50 characters")]
         public string EmailId
         {
            get; set;
         }

  
         [DataType(DataType.Password)]
         [Required(ErrorMessage="Password Required")]
         [DisplayName("Password")]
         [StringLength(30, ErrorMessage = ":Less than 30 characters")]
         public string Password
         {
            get;  set;
         }
 

         public bool IsUserExist(string emailId, string password)
         {
            bool flag =  false;
            var connection = new SqlConnection(connString);
            currentUser = emailId;
            //Order.ShowCurrUser(currentUser);

            connection.Open();
            var command = new SqlCommand("select count(*) from Users where Email='" + emailId + "' and Password='" + password + "'", connection);
          
            flag = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();
            return flag;
         }    
    }
}

 
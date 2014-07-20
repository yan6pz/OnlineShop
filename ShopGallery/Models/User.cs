using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class User
    {
        public static string connString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";

         public User()
        {
            //this.Order = new HashSet<Order>();
        }
         
        
        public int UserID { get; set; }

        [Required(ErrorMessage = "UserName Required:")]
        [DisplayName("User Name:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required:")]
        [DataType(DataType.Password)]
        [DisplayName("Password:")]
        [StringLength(30, ErrorMessage = "Less than 30 characters")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "FirstName Required:")]
        [DisplayName("First Name:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = @"Special Characters   
                                                                        not allowed")]
        [DisplayName("Last Name:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Required:")]
        [DisplayName("Email Id:")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
                                               ErrorMessage = "Email Format is wrong")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string Email { get; set; }

        public User (int id,string name, string Fname, string Lname, string email, string pass)
        {
            this.Email = email;
            this.FirstName = Fname;
            this.LastName = Lname;
            this.UserName = name;
            this.Password = pass;
            this.UserID = id;

        }
        public User(int id, string name, string Fname, string Lname, string email, string pass, bool isAdmin)
        {
            this.UserID = id;
            this.Email = email;
            this.FirstName = Fname;
            this.LastName = Lname;
            this.UserName = name;
            this.Password = pass;
            this.IsAdmin = isAdmin;


        }

        public List<User> ShowUsers()
        {

            using (SqlConnection connect = new SqlConnection(connString))
            {

                SqlCommand select = new SqlCommand("select * from Users", connect);
                select.Connection.Open();
                List<User> users = new List<User>();
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User(int.Parse(reader["UserID"].ToString()),
                                                 reader["UserName"].ToString(),
                                                 reader["Password"].ToString(),
                                                 reader["FirstName"].ToString(),
                                                 reader["LastName"].ToString(),
                                                 reader["Email"].ToString()                                               
                                                 ));

                    }
                }

                return users;
            }
        }
       

        public void CreateUser(string name, string Fname, string Lname,string email,
                                                            string pass,bool admin)
        {
            
            using (SqlConnection connect = new SqlConnection(connString))
            {
                
                SqlCommand insert = new SqlCommand("Insert into Users(UserName,Password,FirstName,LastName,Email,IsAdmin)"+
                   "Values (@name,@pass,@Fname,@Lname,@email,@admin)", connect);

                insert.Parameters.Add(new SqlParameter("@name", name));
                insert.Parameters.Add(new SqlParameter("@pass", pass));
                insert.Parameters.Add(new SqlParameter("@Fname", Fname));
                insert.Parameters.Add(new SqlParameter("@Lname", Lname));
                insert.Parameters.Add(new SqlParameter("@email", email));
                insert.Parameters.Add(new SqlParameter("@admin", admin));
               
                
                    insert.Connection.Open();
                    insert.ExecuteNonQuery();
               
               
                
            }
           
        }
        public static bool IsUserExist(string emailid)
        {
            bool flag = false;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand command = new SqlCommand("select count(*) from Users where Email='" + emailid + "'", connection);
            flag = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();
            return flag;
        }

        public void Delete(int id)
        {
            List<int> oDetIds = new List<int>();
            using (SqlConnection connect = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("delete from Users where UserID=" + id, connect);
                SqlCommand deleteOrders = new SqlCommand("delete from Orders where UserID=" + id, connect);
                SqlCommand selectODet = new SqlCommand("select OrderDetailsID " +
                                                     "from OrderDetails " +
                                                     "join Orders " +
                                                     "on OrderDetails.OrderID=Orders.OrderID " +
                                                     "join Users " +
                                                     "on Orders.UserID=Users.UserID " +
                                                     "where Users.UserID=" + id, connect);
                selectODet.Connection.Open();
                selectODet.ExecuteNonQuery();  
                using(SqlDataReader read=selectODet.ExecuteReader())
                {
                    while (read.Read())
                    {
                        oDetIds.Add(int.Parse(read["OrderDetailsID"].ToString()));
                                             
                    }
                }
                foreach (var oDetId in oDetIds)
                {
                    SqlCommand deleteOrderDet = new SqlCommand("delete from OrderDetails where OrderDetailsID=" + oDetId, connect);
                    deleteOrderDet.ExecuteNonQuery(); 
                }
       
                deleteOrders.ExecuteNonQuery();
                command.ExecuteNonQuery();

            }
        }
      
    }
}
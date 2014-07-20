using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class Register
    {

        public static string connString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";

        [Required(ErrorMessage = "UserName Required:")]
        [DisplayName("User Name:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string UserName { get; set; }

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



        [Required(ErrorMessage = "Password Required:")]
        [DataType(DataType.Password)]
        [DisplayName("Password:")]
        [StringLength(30, ErrorMessage = "Less than 30 characters")]
        public string Password { get; set; }



        [Required(ErrorMessage = "Confirm Password Required:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm not matched.")]
        [Display(Name = "Confirm password:")]
        [StringLength(30, ErrorMessage = "Less than 30 characters")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter Verification Code")]
        [DisplayName("Verification Code:")]
        public string VerCode { get; set; }



        public bool IsUserExist(string emailid)
        {
            bool flag = false;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand command = new SqlCommand("select count(*) from Users where Email='" + emailid + "'", connection);
            flag = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();
            return flag;
        }

        public bool Insert()
        {
            bool flag = false;
            if (!IsUserExist(Email))
            {
                SqlConnection connection = new SqlConnection(connString);
                connection.Open();
                SqlCommand command = new SqlCommand("insert into Users(UserName,Password,FirstName,LastName,Email) values('" + 
                                                UserName + "','" + Password + "','" + FirstName + 
                                                    "','" + LastName + "','" + Email, connection);
                flag = Convert.ToBoolean(command.ExecuteNonQuery());
                connection.Close();
                return flag;
            }
            return flag;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        public User()
        {
            //this.Order = new HashSet<Order>();
        }


        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email { get; set; }

        public User(int id, string name, string Fname, string Lname, string email, string pass)
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
    }
}

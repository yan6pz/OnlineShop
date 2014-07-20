using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Category
    {
        public Category(int id, string name)
        {
            //this.Subcategory = new HashSet<Subcategory>();
            this.CategoryID = id;
            this.CategoryName = name;
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}

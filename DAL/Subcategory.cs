using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Subcategory
    {
        public Subcategory(int subID, string subcategoryName)
        {
            this.SubcategoryID = subID;
            this.SubcategoryName = subcategoryName;
        }

        public int SubcategoryID { get; set; }
        public int CategoryID { get; set; }
        public string SubcategoryName { get; set; }
    }
}

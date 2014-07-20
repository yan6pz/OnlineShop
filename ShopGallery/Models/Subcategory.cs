using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
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
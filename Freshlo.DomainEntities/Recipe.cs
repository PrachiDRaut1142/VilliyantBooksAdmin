using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class Recipe
    {
        public int R_Id { get; set; }
        public string RecipeId { get; set; }
        public string Item_name { get; set; }
        public string Item_type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}

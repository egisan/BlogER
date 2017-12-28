using System;
using System.Collections.Generic;

namespace BloggUppg1ER.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Posts = new HashSet<Posts>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        // public string Description { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}

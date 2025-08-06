using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

using System;

namespace App.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign key

        public string Url { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public Product Product { get; set; } = null!;
    }
}



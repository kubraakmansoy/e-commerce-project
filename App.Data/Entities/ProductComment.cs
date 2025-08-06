using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace App.Data.Entities
{
    public class ProductComment
    {
        public int Id { get; set; }

        // Foreign Keys
        public int ProductId { get; set; }
        public int UserId { get; set; }

        // Properties
        public string Text { get; set; } = null!;
        public byte StarCount { get; set; } // 1–5
        public bool IsConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}


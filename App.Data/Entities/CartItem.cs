using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace App.Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // Properties
        public byte Quantity { get; set; } // min:1
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }






}


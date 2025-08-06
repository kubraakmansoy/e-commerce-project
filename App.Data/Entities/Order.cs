using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Entities;

using System;

namespace App.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        // Foreign key
        public int UserId { get; set; }

        public string OrderCode { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}


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

        public int UserId { get; set; }

        public int ProductId { get; set; }      
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public bool IsPaid { get; set; } = false;

        public bool Enabled { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public bool IsPaid { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}


namespace App.WebUI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public byte StockAmount { get; set; }
        public bool Enabled { get; set; }
    }
}

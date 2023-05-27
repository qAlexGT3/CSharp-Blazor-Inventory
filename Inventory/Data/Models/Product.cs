namespace Inventory.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PartNumber { get; set; }
        public double SellPrice { get; set; }
        public double BuyPrice { get; set; }
    }
}

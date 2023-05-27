namespace Inventory.Data.Models
{
    public class Register
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }

        public string? LocationName { get; set; }
        public string? ProductName { get; set; }
    }
}

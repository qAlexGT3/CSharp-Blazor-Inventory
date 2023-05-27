namespace Inventory.Data.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string? LocationName { get; set; }

        public bool Disabled { get; set; } = false;
    }
}

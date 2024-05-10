namespace Daw.DataLayer.Models
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}

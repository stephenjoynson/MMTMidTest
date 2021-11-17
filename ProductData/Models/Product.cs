namespace ProductData.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }
}

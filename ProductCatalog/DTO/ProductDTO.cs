namespace ProductCatalog.Model
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public List<string> Categories { get; set; } = new();
    }
}

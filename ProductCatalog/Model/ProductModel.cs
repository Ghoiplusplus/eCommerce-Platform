using System.Text.Json.Serialization;

namespace ProductCatalog.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        [JsonIgnore]
        public List<ProductCategoryModel> ProductCategorys { get; set; } = new();
    }
}

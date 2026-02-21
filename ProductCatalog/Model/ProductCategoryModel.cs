namespace ProductCatalog.Model
{
    public class ProductCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductModel> Products { get; set; } = new();

    }
}

namespace ShoppingCart.Model
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}

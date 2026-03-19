namespace apidoga
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
    }
}

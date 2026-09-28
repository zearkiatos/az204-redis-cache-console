namespace RedisCacheConsole.Product.Domain.Model
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal ListPrice { get; set; }

        public ProductModel(string name, decimal listPrice)
        {
            this.Name = name;
            this.ListPrice = listPrice;
        }
    }
}   
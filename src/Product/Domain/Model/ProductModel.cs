namespace RedisCacheConsole.Product.Domain.Model
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal ListPrice { get; set; }

        public ProductModel(string name, decimal priceList)
        {
            this.Name = name;
            this.ListPrice = priceList;
        }
    }
}   
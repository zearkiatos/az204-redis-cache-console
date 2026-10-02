using Microsoft.Data.SqlClient;
using RedisCacheConsole.Product.Domain.Model;
using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Product.Infrastructure.Repository
{
    public class SqlServerProductRepository : ProductRepository
    {
        private string connectionString;

        public List<ProductModel> Fetch(Criterial criterial)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {criterial.Limit} Name, ListPrice FROM SalesLT.Product", connection);
                var reader = command.ExecuteReader();
                var products = new List<ProductModel>();
                while (reader.Read())
                {
                    var product = new ProductModel(
                        name: reader.GetString(0),
                        listPrice: reader.GetDecimal(1)
                    );
                    products.Add(product);
                }
                return products;
            }
        }

        public void Save(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public void Setup(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {

            return new SqlConnection(_configuration.GetConnectionString("sqlconn"));
        }

        public List<Product> GetProducts()
        {
            SqlConnection connection = GetConnection();
            List<Product> _productsList = new List<Product>();
            string sqlStatement = "SELECT * FROM Products";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _productsList.Add(product);
                }
            }
            connection.Close();
            return _productsList;
        }
    }
}

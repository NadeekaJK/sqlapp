using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "az204sqldbserver.database.windows.net";
        private static string db_user  = "azsqlAdminUser";
        private static string db_password = "rosesOnthesil@";
        private static string db_database = "az204sql";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource= db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog= db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection connection= GetConnection();
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

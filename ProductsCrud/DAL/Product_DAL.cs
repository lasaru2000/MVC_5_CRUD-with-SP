using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ProductsCrud.Models;

namespace ProductsCrud.DAL

{
   
    public class Product_DAL
    {
       readonly string connectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();

        public List <Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["Productname"].ToString(),
                        Price = Convert.ToInt32(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    }); ;
                }

            }
            return productList;
        }

        //insert products 

        public bool InsertProducts(Product product) {

            int  id = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts ", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }
 
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> GetProductById(int ProductId)
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductById";
                command.Parameters.AddWithValue("@productId", ProductId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["Productname"].ToString(),
                        Price = Convert.ToInt32(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    }); ;
                }

            }
            return productList;
        }

        public bool UpdateProducts(Product product)
        {

            int id = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProductById ", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }

            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteProductById(int ProductId)
        {
            int id = 0;

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteProductById", connection);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", ProductId);

                connection.Open() ;
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if(id > 0)
            {
                return true;
            }else { return false; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseAccess:IDatabaseAccess
    {
        private readonly string connectionString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";
        public SqlConnection DbProviderFactory
        {
            get { return (SqlConnection)ShopDbConnection.Connection; }
            set { }
        }

        public DatabaseAccess(SqlConnection dbProviderFactory)
        {
            DbProviderFactory = dbProviderFactory;
        }

        public DatabaseAccess()
        {
            // TODO: Complete member initialization
        }


        //Метод за извличане на данни от базата връщащ първата стойност на срещнатите данни в таблицата.
        public T ExecuteScalar<T>(string sqlQuery, ShopDbConnection reportConnection)
        {

            using (var command = this.DbProviderFactory.CreateCommand())
            {
                command.Connection = (SqlConnection)ShopDbConnection.Connection;
                command.Transaction = (SqlTransaction)reportConnection.Transaction;
                command.CommandText = sqlQuery;


                var result = (T)command.ExecuteScalar();

                return result;
            }
        }

        //Метод за извличане на данни от базата връщащ всички стойности на срещнатите данни в таблицата. 
        //Популиращ стойностите в списък от съответния обект,който подаваме като параметър.
        public List<T> ExecuteReader<T>                             (string sqlQuery
                                                                       , Func<System.Data.IDataReader, T> popolateResultCallback)
        {
            var result = new List<T>();
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection) ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction) reportConnection.Transaction;
                    command.CommandText = sqlQuery;

                    using (var reader = command.ExecuteReader( /*CommandBehavior.CloseConnection*/))
                        while (reader != null && reader.Read())
                            result.Add(popolateResultCallback(reader));
                }
            }

            return result;
        }

        public List<T> ExecuteReader<T>(string sqlQuery
                                        ,string id
                                         , Func<IDataReader, T> popolateResultCallback)
        {
            var result = new List<T>();
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection)ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction)reportConnection.Transaction;
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader( /*CommandBehavior.CloseConnection*/))
                        while (reader.Read())
                            result.Add(popolateResultCallback(reader));
                }
            }

            return result;
        }

        public T ExecuteReader<T>(string sqlQuery
                                                                        , int id
                                                                        , ShopDbConnection reportConnection
                                                                        , T inputParameter
                                                                        , Func<IDataReader, T> popolateResultCallback)
        {
            var result = inputParameter;
            // var connectionString                        = UserQueries.CONNECTION_STRING;

            //using (var reportConnection                 = new ReportDbConnection(connectionString))

            using (var command = this.DbProviderFactory.CreateCommand())
            {

                command.Connection = (SqlConnection)ShopDbConnection.Connection;
                command.Transaction = (SqlTransaction)reportConnection.Transaction;
                command.CommandText = sqlQuery;
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        result = popolateResultCallback(reader);
            }
            return result;
        }

        public bool Create(string sqlQuery,Product product)
        {
            int result;
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection)ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction)reportConnection.Transaction;
                    command.CommandText = sqlQuery;
        
                    command.Parameters.Add(new SqlParameter("@newName", product.Name));
                    command.Parameters.Add(new SqlParameter("@newColor", product.Color));
                    command.Parameters.Add(new SqlParameter("@newUP", product.UnitPrice));
                    command.Parameters.Add(new SqlParameter("@size", product.Size));
                    command.Parameters.Add(new SqlParameter("@newWeight", product.Weight));
                    command.Parameters.Add(new SqlParameter("@discount", product.IsDiscounted));
                    command.Parameters.Add(new SqlParameter("@subcatID", product.SubcategoryID));
                    command.Parameters.Add(new SqlParameter("@image", product.ImageProduct));


                    result = command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);
                }
            }

            return result == 1;
        }

        public bool CreateOrder(string sqlQuery, Product product, int userId,decimal? qty)
        {
            {
                int result;
                decimal? price = qty * product.UnitPrice;
                
                using (var reportConnection = new ShopDbConnection(connectionString))
                {
                    
                    using (var command = this.DbProviderFactory.CreateCommand())
                    {
                        command.Connection = (SqlConnection)ShopDbConnection.Connection;
                        command.Transaction = (SqlTransaction)reportConnection.Transaction;
                        command.CommandText = sqlQuery;

                        

                        command.Parameters.Add(new SqlParameter("@userId", userId));
                        command.Parameters.Add(new SqlParameter("@price", price));
                        command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                        command.Parameters.Add(new SqlParameter("@qty", qty));
                        command.Parameters.Add(new SqlParameter("@productId", product.ProductID));
                        result=command.ExecuteNonQuery();
                    }
                    CreateOrderDetails( product.ProductID, price, qty, reportConnection);
                }

                return result == 1;
            }
        }

        private void CreateOrderDetails( int productId, decimal? price, decimal? qty, ShopDbConnection reportConnection)
        {
            string orderId="";
            using (var command = this.DbProviderFactory.CreateCommand())
            {
                
                command.Connection = (SqlConnection)ShopDbConnection.Connection;
                command.Transaction = (SqlTransaction)reportConnection.Transaction;
                command.CommandText = SqlCommands.GET_ORDERID;
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        orderId = reader["OrderID"].ToString();
              
            }
            using (var command = this.DbProviderFactory.CreateCommand())
            {
                command.Connection = (SqlConnection)ShopDbConnection.Connection;
                command.Transaction = (SqlTransaction)reportConnection.Transaction;
                command.CommandText = SqlCommands.CREATE_ORDERDETAILS;

                command.Parameters.Add(new SqlParameter("@orderId", orderId));
                command.Parameters.Add(new SqlParameter("@price", price));
                command.Parameters.Add(new SqlParameter("@productId", productId));
                command.Parameters.Add(new SqlParameter("@qty", qty));

                command.ExecuteNonQuery();
            }
        }


        public bool UpdateProduct( int id,string prodName, string color, decimal unitPrice,
                                                  string size, int weight)
        {
            int result;
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection) ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction) reportConnection.Transaction;

                    if (prodName != null)
                    {
                        command.CommandText += SqlCommands.UPDATE_PRODUCT_name;
                        command.Parameters.Add(new SqlParameter("@newName", prodName));
                        command.Parameters.AddWithValue("@idN", id);
                    }
                    if (color != null)
                    {
                        command.CommandText += SqlCommands.UPDATE_PRODUCT_color;
                        command.Parameters.Add(new SqlParameter("@newColor", color));
                        command.Parameters.AddWithValue("@idC", id);
                    }
                    if (unitPrice != 0)
                    {
                        command.CommandText += SqlCommands.UPDATE_PRODUCT_price;
                        command.Parameters.Add(new SqlParameter("@newUp", unitPrice));
                        command.Parameters.AddWithValue("@idP", id);
                    }
                    if (size != null)
                    {
                        command.CommandText += SqlCommands.UPDATE_PRODUCT_size;
                        command.Parameters.Add(new SqlParameter("@newSize", size));
                        command.Parameters.AddWithValue("@idS", id); 
                    }
                    if (weight != 0)
                    {
                        command.CommandText += SqlCommands.UPDATE_PRODUCT_weight;
                        command.Parameters.Add(new SqlParameter("@newWeight", weight));
                        command.Parameters.AddWithValue("@idW", id);
                        
                    }
                    
                    //command.Parameters.Add("@idN", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //command.Parameters.AddWithValue("@idN", id);


                result = command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);
                }
            }

            return result == 1;
        }

        public bool DeleteOrders(string sqlQuery, int id)
        {
            int result;
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                DeleteOrderDetails(reportConnection, id);
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection)ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction)reportConnection.Transaction;
                    command.CommandText = sqlQuery;

                    command.Parameters.Add(new SqlParameter("@idO", id));
                    result = command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);
                }
            }

            return result == 1;
        }

        private void DeleteOrderDetails(ShopDbConnection reportConnection, int id)
        {
            using (var command = this.DbProviderFactory.CreateCommand())
            {
                command.Connection = (SqlConnection)ShopDbConnection.Connection;
                command.Transaction = (SqlTransaction)reportConnection.Transaction;
                command.CommandText = SqlCommands.DELETE_ORDERDETAILS;

                command.Parameters.Add(new SqlParameter("@idOd", id));

                command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);

            }

        }

        public bool Delete(string sqlQuery, int id)
        {
            int result;
            using (var reportConnection = new ShopDbConnection(connectionString))
            {
                DeleteFromSubcategories(reportConnection, id);
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection)ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction)reportConnection.Transaction;
                    command.CommandText = sqlQuery;

                    command.Parameters.Add(new SqlParameter("@id", id));
                 


                    result = command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);
                }
            }

            return result == 1;
        }
        private void DeleteFromSubcategories(ShopDbConnection reportConnection, int id)
        {
                using (var command = this.DbProviderFactory.CreateCommand())
                {
                    command.Connection = (SqlConnection)ShopDbConnection.Connection;
                    command.Transaction = (SqlTransaction)reportConnection.Transaction;
                    command.CommandText = SqlCommands.DELETE_FROM_SUBCATEGORIES;

                    command.Parameters.Add(new SqlParameter("@id", id));

                    command.ExecuteNonQuery( /*CommandBehavior.CloseConnection*/);
                
            }

        }
    }
}

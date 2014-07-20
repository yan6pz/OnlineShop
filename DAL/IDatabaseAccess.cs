using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDatabaseAccess
    {
        T ExecuteScalar<T>(string sqlQuery, ShopDbConnection reportConnection);


        List<T> ExecuteReader<T>(
                                                                          string sqlQuery
                                                                        , Func<IDataReader, T> popolateResultCallback);
        List<T> ExecuteReader<T>(
                                                                          string sqlQuery
                                                                        ,string id
                                                                        , Func<IDataReader, T> popolateResultCallback);


        T ExecuteReader<T>(
                                                                           string sqlQuery
                                                                        , int id
                                                                        , ShopDbConnection reportConnection
                                                                        , T inputParameter
                                                                        , Func<IDataReader, T> popolateResultCallback);
        bool Create(string sqlQuery,Product product);

        bool CreateOrder(string sqlQuery, Product product,int userId,decimal? qty);

        bool Delete(string sqlQuery,int id);

        bool UpdateProduct( int id, string prodName
                            , string color, decimal unitPrice
                            ,string size, int weight);

        bool DeleteOrders(string sqlQuery, int id);
    }
}

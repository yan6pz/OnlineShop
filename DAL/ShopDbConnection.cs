using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ShopDbConnection:IDisposable
    {

        private bool disposed;

        public static DbConnection                         Connection                      { get; private set; }

        public SqlConnection                             ProviderFactory                 { get; private set; }

        public DbTransaction                               Transaction                     { get; private set; }

        public ShopDbConnection( 
                                                                                     string connectionString )
        {
            var connection                                  = new SqlConnection(connectionString);

            this.ProviderFactory                            = connection;
            Connection                                      = connection;
            this.ProviderFactory.Open();
        }

        public void                                         CommitTransaction               ()
        {
            this.Transaction.Commit();
        }

        public void                                         Dispose                         ()
        {
            if (this.disposed) 
                return;

            this.ProviderFactory.Dispose();

            this.CloseConnection();
                
            Connection.Dispose();

            this.disposed                                   = true;
        }

        private void                                        CloseConnection                 ()
        {
            if (Connection.State == ConnectionState.Closed) 
                return;
            
            Connection.Close();
        }

        private void OpenConnection()
        {
            Connection.Open();
            this.Transaction                                 = Connection.BeginTransaction( IsolationLevel.ReadUncommitted );
        }
    }
}

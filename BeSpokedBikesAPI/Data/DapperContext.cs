using Microsoft.Data.SqlClient;
using System.Data;

namespace BeSpokedBikesAPI.Data
{

    //public class DapperContext
    //{
    //    private readonly string _connectionString;

    //    public DapperContext(string connectionString)
    //    {
    //        _connectionString = connectionString;
    //    }

    //    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    //}

    public class DapperContext(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        //public IDbConnection CreateConnection() => new SqlConnection(_connectionString);


        public IDbConnection CreateConnection()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString)
            {
                ApplicationIntent = ApplicationIntent.ReadWrite, // Or whichever intent is appropriate for your application
                ConnectRetryCount = 3 // Or adjust the retry count as needed
            };

            return new SqlConnection(connectionStringBuilder.ConnectionString);
        }
    }

}



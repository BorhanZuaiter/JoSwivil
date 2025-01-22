using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public class InitConnection
    {
        public static SqlCommand MakeConn(IConfiguration configuration)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = configuration["ConnectionStrings:DefaultConnection"];

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 60000;
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

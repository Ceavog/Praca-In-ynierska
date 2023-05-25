//using MySql.Data.MySqlClient;
using System;
using MySqlConnector;

namespace CloudSql
{
    /// <summary>
    /// this class is here in case of hosting on google cloud
    /// </summary>
    public class MySqlUnix
    {
        public static MySqlConnectionStringBuilder NewMysqlUnixSocketConnectionString()
        {
            // Equivalent connection string:
            // "Server=<INSTANCE_UNIX_SOCKET>;Uid=<DB_USER>;Pwd=<DB_PASS>;Database=<DB_NAME>;Protocol=unix"
            var connectionString = new MySqlConnectionStringBuilder
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = MySqlSslMode.None,

                // Note: Saving credentials in environment variables is convenient, but not
                // secure - consider a more secure solution such as
                // Cloud Secret Manager (https://cloud.google.com/secret-manager) to help
                // keep secrets safe.
                Server = "/cloudsql/pos-364415:europe-central2:possql", 
                UserID = "pieczywo",   
                //Password = Environment.GetEnvironmentVariable("DB_PASS"), 
                Database = "pieczywoDB", 
                ConnectionProtocol = MySqlConnectionProtocol.UnixSocket,
                Pooling = true
            };
            // Specify additional properties here.
            return connectionString;
        }
    }
}
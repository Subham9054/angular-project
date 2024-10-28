using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient ;
namespace Config.Repository.Factory
{
    public class db_PHED_CGRCConnectionFactory:Idb_PHED_CGRCConnectionFactory
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public db_PHED_CGRCConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the get connection.
        /// </summary>
        /// <value>The get connection.</value>
        public IDbConnection GetConnection => new MySqlConnection(_connectionString);
    }
}

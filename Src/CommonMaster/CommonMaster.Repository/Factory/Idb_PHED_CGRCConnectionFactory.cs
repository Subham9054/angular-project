using System.Data;

namespace CommonMaster.Repository.Factory
{
     public interface Idb_PHED_CGRCConnectionFactory
    {
        /// <summary>
        /// Gets the get connection.
        /// </summary>
        /// <value>The get connection.</value>
        IDbConnection GetConnection { get; }
    }
}

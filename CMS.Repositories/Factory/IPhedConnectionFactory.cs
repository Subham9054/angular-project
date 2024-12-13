using System.Data;

namespace CMS.Repositories.Factory
{
    public interface IPhedConnectionFactory
    {
        /// <summary>
        /// Gets the get connection.
        /// </summary>
        /// <value>The get connection.</value>
        IDbConnection GetConnection { get; }
    }
}

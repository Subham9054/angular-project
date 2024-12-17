using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminConsole.Repository.Factory;
using AdminConsole.Repository.Repositories.Interfaces;
using AdminConsole.Repository.Repositories.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AdminConsole.Repository.Container
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory = new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
         
            services.AddSingleton<Idb_PHED_CGRCConnectionFactory>(db_PHED_CGRCconnectionFactory);
            services.AddScoped<IAdminconsRepository, AdminconsRepository>();
        }
    }
}

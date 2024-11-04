using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Dropdown.Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropdown.Repository.Repositories.Interfaces;
using Dropdown.Repository.Repositories.Repository;
using System.Configuration;

namespace Dropdown.Repository.Container
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory = new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
            services.AddSingleton<Idb_PHED_CGRCConnectionFactory>(db_PHED_CGRCconnectionFactory);
            services.AddScoped<IDropdownRepository, DropdownRepository>();
        }
    }
}

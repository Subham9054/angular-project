using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CMS.Repository.Factory;
namespace CMS.Repository.Container
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory = new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
            services.AddSingleton<Idb_PHED_CGRCConnectionFactory>(db_PHED_CGRCconnectionFactory);

            //services.AddScoped<IMANAGE_COMPLAINTDETAILS_CONFIGRepository, MANAGE_COMPLAINTDETAILS_CONFIGRepository>();
            //Write code here
        }
    }
}

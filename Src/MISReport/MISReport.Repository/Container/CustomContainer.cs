using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using MISReport.Repository.Factory;
using MISReport.Repository.Repositories.Interfaces.MANAGE_HOLIDAYSLIST_CONFIG;
using MISReport.Repository.Interfaces.MANAGE_HOLIDAYSLIST_CONFIG;
namespace MISReport.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory=new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
		services.AddSingleton<Idb_PHED_CGRCConnectionFactory> (db_PHED_CGRCconnectionFactory);

		services.AddScoped<IMANAGE_HOLIDAYSLIST_CONFIGRepository, MANAGE_HOLIDAYSLIST_CONFIGRepository>();
			//Write code here
		}
	}
}

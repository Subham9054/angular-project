using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using Config.Repository.Factory;
using Config.Repository.Repositories.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS;
using Config.Repository.Interfaces.MANAGE_ESCALATION_CONFIGDETAILS;
namespace Config.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory=new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
		services.AddSingleton<Idb_PHED_CGRCConnectionFactory> (db_PHED_CGRCconnectionFactory);

		services.AddScoped<IMANAGE_ESCALATION_CONFIGDETAILSRepository, MANAGE_ESCALATION_CONFIGDETAILSRepository>();
			//Write code here
		}
	}
}

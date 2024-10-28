using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using CommonMaster.Repository.Factory;
using CommonMaster.Repository.Repositories.Interfaces.MANAGE_CATEGORYMASTER;
using CommonMaster.Repository.Interfaces.MANAGE_CATEGORYMASTER;
using CommonMaster.Repository.Repositories.Interfaces.MANAGE_SUBCATEGORYMASTER;
using CommonMaster.Repository.Interfaces.MANAGE_SUBCATEGORYMASTER;
namespace CommonMaster.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory=new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
		services.AddSingleton<Idb_PHED_CGRCConnectionFactory> (db_PHED_CGRCconnectionFactory);

		services.AddScoped<IMANAGE_CATEGORYMASTERRepository, MANAGE_CATEGORYMASTERRepository>();
		services.AddScoped<IMANAGE_SUBCATEGORYMASTERRepository, MANAGE_SUBCATEGORYMASTERRepository>();
			//Write code here
		}
	}
}

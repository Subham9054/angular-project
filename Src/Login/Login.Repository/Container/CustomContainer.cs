using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using GMS.Repository.Factory;
using Login.Repository.Repositories.Interfaces;
using Login.Repository.Repositories.Repository;

namespace GMS.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory=new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
		services.AddSingleton<Idb_PHED_CGRCConnectionFactory> (db_PHED_CGRCconnectionFactory);

		services.AddScoped<ILoginRepository, LoginRepository>();
			//Write code here
		}
	}
}

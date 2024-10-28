using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using GMS.Repository.Factory;
using GMS.Repository.Repositories.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
using GMS.Repository.Interfaces.MANAGE_COMPLAINTDETAILS_CONFIG;
namespace GMS.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		Idb_PHED_CGRCConnectionFactory db_PHED_CGRCconnectionFactory=new db_PHED_CGRCConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
		services.AddSingleton<Idb_PHED_CGRCConnectionFactory> (db_PHED_CGRCconnectionFactory);

		services.AddScoped<IMANAGE_COMPLAINTDETAILS_CONFIGRepository, MANAGE_COMPLAINTDETAILS_CONFIGRepository>();
			//Write code here
		}
	}
}

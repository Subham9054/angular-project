using CMS.Repositories.Factory;
using CMS.Repositories.Repositories.CmsRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CMS.Repositories.Container
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            IPhedConnectionFactory phedConnectionFactory = new PhedConnectionFactory(configuration.GetConnectionString("DBconnectiondb_PHED_CGRC"));
            services.AddSingleton<IPhedConnectionFactory>(phedConnectionFactory);
            
            //Write code here
            services.AddScoped<IContentManagementRepository, ContentManagementRepository>();
        }
    }
}

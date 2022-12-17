using Microsoft.EntityFrameworkCore;
using oldprogrammer.authentication.httpclients.EmailClient;
using oldprogrammer_authentication.domain.Base;
using oldprogrammer_authentication.domain.Context;
using oldprogrammer_authentication.domain.Repositories;
using oldprogrammer_authentication.services.Register;
using oldprogrammer_authetication.core.Domains;
using oldprogrammer_authetication.core.HttpClients;
using oldprogrammer_authetication.core.Models;
using oldprogrammer_authetication.core.Repositories;

namespace oldprogrammer_authentication
{
    public static class Services
    {
        public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OldProgrammerDB"));
            });
        }
        public static void ConfigurationDependencies(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IRegisterService, RegisterService>();
            
            //Repositories
            services.AddScoped<IRegisterRepository, RegisterRepository>();

            //Domains
            services.AddScoped<IAuthenticationUserDomain, AuthenticationUserDomain>();

            //HttpClients
            services.AddHttpClient<IEmailHttpClient, EmailHttpClient>();
        }
    }
}

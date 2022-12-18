using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oldprogrammer.authentication.httpclients.EmailClient;
using oldprogrammer_authentication.domain.Context;
using oldprogrammer_authentication.Security;
using oldprogrammer_authentication.services.Register;
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
            }).AddIdentity<AuthenticationUser, IdentityRole>(setup =>
            {
                setup.Password.RequiredLength = 8;
                setup.SignIn.RequireConfirmedEmail = true;
                setup.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AuthenticationContext>()
            .AddDefaultTokenProviders();

            services.ConfigurationDependencies();
        }
        public static void ConfigurationDependencies(this IServiceCollection services)
        {
            services.AddHttpClient();
            //Singletons
            //services.AddSingleton<TokenProvider>();

            //Services
            services.AddScoped<IRegisterService, RegisterService>();
            
            //Repositories
            services.AddScoped<IRegisterRepository, RegisterRepository>();

            //Domains

            //HttpClients
            services.AddScoped<IEmailHttpClient, EmailHttpClient>();
        }
    }
}

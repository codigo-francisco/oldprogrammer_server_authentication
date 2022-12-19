using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oldprogrammer.authentication.httpclients.EmailClient;
using oldprogrammer_authentication.domain.Context;
using oldprogrammer_authentication.services.Register;
using oldprogrammer_authetication.core.HttpClients;
using oldprogrammer_authetication.core.Models;
using oldprogrammer_authetication.core.Repositories;

namespace oldprogrammer_authentication
{
    public static class Services
    {
        public static void ConfigureContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthenticationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("OldProgrammerDB"),
                    db => db.MigrationsAssembly("oldprogrammer.migrations.oldprogrammerdb"));
            }).AddIdentity<AuthenticationUser, IdentityRole>(setup =>
            {
                setup.Password.RequiredLength = 8;
                setup.SignIn.RequireConfirmedEmail = true;
                setup.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AuthenticationContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigurationDependencies();
        }
        public static void ConfigurationDependencies(this IServiceCollection services)
        {
            services.AddHttpClient();
            //Singletons
            //services.AddSingleton<TokenProvider>();

            //Services
            services.AddScoped<IRegisterService, RegistrationService>();
            
            //Repositories
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            //Domains

            //HttpClients
            services.AddScoped<IEmailHttpClient, EmailHttpClient>();
        }
        public static void AddAllowedCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Default", policy =>
                {
                    string[] origins = builder.Configuration.GetSection("AllowedCors").Get<string[]>();
                    policy.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using oldprogrammer_authentication.domain.Context;

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
    }
}

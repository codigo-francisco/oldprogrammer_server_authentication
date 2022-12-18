using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using oldprogrammer_authetication.core.Models;

namespace oldprogrammer_authentication.domain.Context
{
    public class AuthenticationContext : IdentityDbContext<AuthenticationUser>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> dbContextOptions) 
            : base(dbContextOptions)
        {
            
        }

        public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }
    }
}

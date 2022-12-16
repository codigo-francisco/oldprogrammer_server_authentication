using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace oldprogrammer_authentication.domain.Context
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> dbContextOptions) 
            : base(dbContextOptions)
        {
            
        }
    }
}

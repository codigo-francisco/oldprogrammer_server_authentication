using Microsoft.EntityFrameworkCore;
using oldprogrammer_authentication.domain.Base;
using oldprogrammer_authentication.domain.Context;
using oldprogrammer_authetication.core.Domains;
using oldprogrammer_authetication.core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authentication.domain.Repositories
{
    public class AuthenticationUserRepository : RepositoryBase<AuthenticationUser, AuthenticationContext>, IAuthenticationUserRepository
    {
        public AuthenticationUserRepository(AuthenticationContext _context) : base(_context)
        {
        }

        DbContext IAuthenticationUserRepository.GetContext()
        {
            return Context;
        }
    }
}

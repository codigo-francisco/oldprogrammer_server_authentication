using Microsoft.EntityFrameworkCore;
using oldprogrammer_authetication.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Domains
{
    public interface IAuthenticationUserRepository : IRepository<AuthenticationUser>
    {
        DbContext GetContext();
    }
}
